// ClockDlg.h : ヘッダー ファイル
//

#if !defined(AFX_CLOCKDLG_H__B7398BE7_12C6_11D4_AFC1_00104B266938__INCLUDED_)
#define AFX_CLOCKDLG_H__B7398BE7_12C6_11D4_AFC1_00104B266938__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


/////////////////////////////////////////////////////////////////////////////
// CClockDlg ダイアログ

class CClockDlg : public CDialog
{
// 構築
public:
	CClockDlg(CWnd* pParent = NULL);	// 標準のコンストラクタ

    double sec;//変数の追加

// ダイアログ データ
	//{{AFX_DATA(CClockDlg)
	enum { IDD = IDD_CLOCK_DIALOG };
	CString	m_sec;
	//}}AFX_DATA

	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CClockDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV のサポート
	//}}AFX_VIRTUAL

// インプリメンテーション
protected:
	HICON m_hIcon;

	// 生成されたメッセージ マップ関数
	//{{AFX_MSG(CClockDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnStart();
	afx_msg void OnStop();
	afx_msg void OnTimer(UINT nIDEvent);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
private:
	
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_CLOCKDLG_H__B7398BE7_12C6_11D4_AFC1_00104B266938__INCLUDED_)
