// ClockDlg.h : �w�b�_�[ �t�@�C��
//

#if !defined(AFX_CLOCKDLG_H__B7398BE7_12C6_11D4_AFC1_00104B266938__INCLUDED_)
#define AFX_CLOCKDLG_H__B7398BE7_12C6_11D4_AFC1_00104B266938__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


/////////////////////////////////////////////////////////////////////////////
// CClockDlg �_�C�A���O

class CClockDlg : public CDialog
{
// �\�z
public:
	CClockDlg(CWnd* pParent = NULL);	// �W���̃R���X�g���N�^

    double sec;//�ϐ��̒ǉ�

// �_�C�A���O �f�[�^
	//{{AFX_DATA(CClockDlg)
	enum { IDD = IDD_CLOCK_DIALOG };
	CString	m_sec;
	//}}AFX_DATA

	// ClassWizard �͉��z�֐��̃I�[�o�[���C�h�𐶐����܂��B
	//{{AFX_VIRTUAL(CClockDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV �̃T�|�[�g
	//}}AFX_VIRTUAL

// �C���v�������e�[�V����
protected:
	HICON m_hIcon;

	// �������ꂽ���b�Z�[�W �}�b�v�֐�
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
// Microsoft Visual C++ �͑O�s�̒��O�ɒǉ��̐錾��}�����܂��B

#endif // !defined(AFX_CLOCKDLG_H__B7398BE7_12C6_11D4_AFC1_00104B266938__INCLUDED_)
