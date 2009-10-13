// Clock.h : CLOCK アプリケーションのメイン ヘッダー ファイルです。
//

#if !defined(AFX_CLOCK_H__B7398BE5_12C6_11D4_AFC1_00104B266938__INCLUDED_)
#define AFX_CLOCK_H__B7398BE5_12C6_11D4_AFC1_00104B266938__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// メイン シンボル

/////////////////////////////////////////////////////////////////////////////
// CClockApp:
// このクラスの動作の定義に関しては Clock.cpp ファイルを参照してください。
//

class CClockApp : public CWinApp
{
public:
	CClockApp();

// オーバーライド
	// ClassWizard は仮想関数のオーバーライドを生成します。
	//{{AFX_VIRTUAL(CClockApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// インプリメンテーション

	//{{AFX_MSG(CClockApp)
		// メモ - ClassWizard はこの位置にメンバ関数を追加または削除します。
		//        この位置に生成されるコードを編集しないでください。
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ は前行の直前に追加の宣言を挿入します。

#endif // !defined(AFX_CLOCK_H__B7398BE5_12C6_11D4_AFC1_00104B266938__INCLUDED_)
