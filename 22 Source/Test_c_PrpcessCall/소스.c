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


	//fork리눅스 함수
	//int   counter = 0;

	//switch (fork())
	//{
	//	case -1:
	//	{
	//			   printf("자식 프로세스 생성 실패n");
	//			   return -1;
	//	}
	//	case 0:
	//	{
	//			  printf("저는 자식 프로세스로 system()을 호출하겠습니다.n");
	//			  system("ls -al /");
	//			  break;
	//	}
	//	default:
	//	{
	//			   printf("저는 부모 프로세스로 카운트를 하겠습니다.n");
	//			   while (1)
	//			   {
	//				   printf("부모: %dn", counter++);
	//				   //sleep(1);
	//			   }
	//	}
	//}
}

//printf("Hello, world!\n");
//printf("Hello, world!\n");
//system("C:\\femm42\\bin\\femm.exe&");
//printf("ByeBye\n");