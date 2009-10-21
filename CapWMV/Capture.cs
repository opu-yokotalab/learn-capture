/****************************************************************************
While the underlying libraries are covered by LGPL, this sample is released 
as public domain.  It is distributed in the hope that it will be useful, but 
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
or FITNESS FOR A PARTICULAR PURPOSE.  
*****************************************************************************/


using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Runtime.InteropServices;
using System.Data;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;


using DirectShowLib;




namespace AsfFilter
{
	/// <summary> Summary description for MainForm. </summary>
    //動画キャプチャ
    public class Capture: IDisposable
	{
        #region Member variables

        /// <summary> graph builder interface. </summary>
        /// フィルタ グラフを作成するメソッドを提供する 
        /// フィルタグラフは特定のタスクを行うために設定されたフィルタの集合体
        /// (この場合はWMV形式でのキャプチャ)
        /// IFilterGraph インターフェイスおよび IGraphBuilder インターフェイスを拡張する。
		private IFilterGraph2 m_FilterGraph = null;
        //フィルタ グラフを通るデータ フローを制御するメソッドを提供する。
        //ここには、グラフを実行、ポーズ、停止するメソッドが含まれる。
        IMediaControl m_mediaCtrl = null;

        private ICaptureGraphBuilder2 capGraph = null;//キャプチャグラフ
        //private IBaseFilter capFilter;
        

        /// <summary> Set by async routine when it captures an image </summary>
        /// 画像キャプチャ時、非同期ルーチンによってセットされる
        private bool m_bRunning = false;

#if DEBUG
        // Running object table classes to show the graphs in GraphEdt
        DsROTEntry m_rot = null;        

#endif
        
        #endregion

        /// <summary> release everything. </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            CloseInterfaces();
        }

        ~Capture()
        {
            Dispose();
        }

        /// <summary>
        /// Create capture object
        /// </summary>
        /// <param name="iDeviceNum">Zero based index of capture device</param>
        /// iDeviceNum：０番目が基となるキャプチャデバイスのインデックス
        /// <param name="szFileName">Output ASF file name</param>
        /// szFileName：出力するファイルの名前
        public Capture(int iDeviceNum, string szOutputFileName)
        {
            DsDevice [] capDevices;

            // Get the collection of video devices
            capDevices = DsDevice.GetDevicesOfCat( FilterCategory.VideoInputDevice );

            //デバイスが見つからなかったら
            if (iDeviceNum + 1 > capDevices.Length)
            {
                throw new Exception("No video capture devices found at that index!");
            }

            try
            {
                // Set up the capture graph
                //SetupGraph(capDevices[デバイスの番号],キャプチャ時のファイルネーム)
                SetupGraph( capDevices[iDeviceNum], szOutputFileName);

                m_bRunning = false;
            }
            catch
            {
                Dispose();
                throw;
            }
        }


        // Start the capture graph
        public void Start()
        {
            if (!m_bRunning)
            {
                int hr = m_mediaCtrl.Run();
                //システムリソース不足
                Marshal.ThrowExceptionForHR( hr );

                m_bRunning = true;
            }
        }

        // Pause the capture graph.
        // Running the graph takes up a lot of resources.  Pause it when it
        // isn't needed.
        public void Pause()
        {
            if (m_bRunning)
            {
                IMediaControl mediaCtrl = m_FilterGraph as IMediaControl;

                int hr = mediaCtrl.Pause();
                Marshal.ThrowExceptionForHR( hr );

                m_bRunning = false;
            }
        }

        /// <summary> build the capture graph. </summary>
        /// 
        //publicにすればShowCapPinDialogで参照して使えるかも
        private void SetupGraph(DsDevice dev, string szOutputFileName)
		{
            int hr;

            IBaseFilter capFilter = null;
            IBaseFilter asfWriter = null;
		    //ICaptureGraphBuilder2 capGraph = null;
            //ビデオキャプチャ＆編集用のメソッドを備えたキャプチャグラフビルダ

            // Get the graphbuilder object
            m_FilterGraph = (IFilterGraph2)new FilterGraph();

#if DEBUG
            m_rot = new DsROTEntry( m_FilterGraph );
#endif

            try
            {
                // Get the ICaptureGraphBuilder2
                capGraph = (ICaptureGraphBuilder2) new CaptureGraphBuilder2();

                // Start building the graph
                hr = capGraph.SetFiltergraph( m_FilterGraph );
                Marshal.ThrowExceptionForHR( hr );

                // Add the capture device to the graph
                hr = m_FilterGraph.AddSourceFilterForMoniker(dev.Mon, null, dev.Name, out capFilter);
                Marshal.ThrowExceptionForHR( hr );

                asfWriter = ConfigAsf(capGraph, szOutputFileName);

                hr = capGraph.RenderStream(null, null, capFilter, null, asfWriter);
                Marshal.ThrowExceptionForHR( hr );

                m_mediaCtrl = m_FilterGraph as IMediaControl;

                
            }
            finally
            {
                if (capFilter != null)
                {
                    Marshal.ReleaseComObject(capFilter);
                    capFilter = null;
                }
                if (asfWriter != null)
                {
                    Marshal.ReleaseComObject(asfWriter);
                    asfWriter = null;
                }
                if (capGraph != null)
                {
                    Marshal.ReleaseComObject(capGraph);
                    capGraph = null;
                }
            }
        }

        private IBaseFilter ConfigAsf(ICaptureGraphBuilder2 capGraph, string szOutputFileName)
        {
            IFileSinkFilter pTmpSink = null;
            IBaseFilter asfWriter = null;

            int hr = capGraph.SetOutputFileName( MediaSubType.Asf, szOutputFileName, out asfWriter, out pTmpSink);
            Marshal.ThrowExceptionForHR( hr );

            try
            {
                IConfigAsfWriter lConfig = asfWriter as IConfigAsfWriter;

                // Windows Media Video 8 for Dial-up Modem (No audio, 56 Kbps)
                // READ THE README for info about using guids
                Guid cat = new Guid(0x6E2A6955, 0x81DF, 0x4943, 0xBA, 0x50, 0x68, 0xA9, 0x86, 0xA7, 0x08, 0xF6);

                hr = lConfig.ConfigureFilterUsingProfileGuid(cat);
                Marshal.ThrowExceptionForHR( hr );
            }
            finally
            {
                Marshal.ReleaseComObject(pTmpSink);
            }

            return asfWriter;
        }

        /// <summary> Shut down capture </summary>
		private void CloseInterfaces()
		{
            int hr;

            try
			{
				if( m_mediaCtrl != null )
				{
                    // Stop the graph
                    hr = m_mediaCtrl.Stop();
                    m_bRunning = false;
				}
			}
			catch
			{
			}

#if DEBUG
            // Remove graph from the ROT
            if ( m_rot != null )
            {
                m_rot.Dispose();
                m_rot = null;
            }
#endif

            if (m_FilterGraph != null)
            {
                Marshal.ReleaseComObject(m_FilterGraph);
                m_FilterGraph = null;
            }
        }

        //public void ShowPropertyPages()
        //{
        //    DShowNET.DsUtils.ShowCapPinDialog(capGraph,capFilter,this.handle);
        //}
    }
    
}
