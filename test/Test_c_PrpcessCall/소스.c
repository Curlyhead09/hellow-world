#include <windows.h>
#include <stdio.h>

int main()
{
	STARTUPINFO si;
	PROCESS_INFORMATION pi;

	memset((void *)&si, 0x00, sizeof(si));
	memset((void *)&pi, 0x00, sizeof(pi));

	printf("Start\n");

	CreateProcess("C:\\femm42\\bin\\femm.exe",
		NULL,
		NULL,
		NULL,
		0,
		NORMAL_PRIORITY_CLASS,
		NULL,
		NULL,
		&si,
		&pi);
	WaitForSingleObject(pi.hProcess, INFINITE);
	printf("End\n");
	return 1;


	//fork������ �Լ�
	//int   counter = 0;

	//switch (fork())
	//{
	//	case -1:
	//	{
	//			   printf("�ڽ� ���μ��� ���� ����n");
	//			   return -1;
	//	}
	//	case 0:
	//	{
	//			  printf("���� �ڽ� ���μ����� system()�� ȣ���ϰڽ��ϴ�.n");
	//			  system("ls -al /");
	//			  break;
	//	}
	//	default:
	//	{
	//			   printf("���� �θ� ���μ����� ī��Ʈ�� �ϰڽ��ϴ�.n");
	//			   while (1)
	//			   {
	//				   printf("�θ�: %dn", counter++);
	//				   //sleep(1);
	//			   }
	//	}
	//}
}

//printf("Hello, world!\n");
//printf("Hello, world!\n");
//system("C:\\femm42\\bin\\femm.exe&");
//printf("ByeBye\n");