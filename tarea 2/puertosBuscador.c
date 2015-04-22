#include <stdio.h>
#include <unistd.h>
#include <errno.h>
#include <netdb.h>
#include <string.h>
#include <sys/types.h>
#include <sys/socket.h>
 
 
int main(int argc, char *argv[]) {                  
    printf("\n\n");
 
    int sockid;
    int connid;
    int pcon = 1;
    int pa = 0;
    struct sockaddr_in conect;
    conect.sin_family = AF_INET;
    conect.sin_addr.s_addr = inet_addr("127.0.0.1");
    bzero(&(conect.sin_zero), 8);
    for (pcon = 0; pcon != 9000; pcon++) {
        sockid = socket(AF_INET,SOCK_STREAM,0);
        conect.sin_port = htons(pcon);
        connid = connect(sockid, (struct sockaddr *)&conect, sizeof(struct sockaddr));
        if (connid != -1) {
            printf("Puerto %d.................... ABIERTO \n",pcon);
            pa++;
        }
	close(connid);
	close(sockid);
    }
 
    printf("\n\n");
    printf("Scann terminado... %d puertos abiertos",pa);
    printf("\n\n");
    return 0;
}

