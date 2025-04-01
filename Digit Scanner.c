#include <stdio.h>

void coutCharchetrs(char x[]);
int main(){
    char x[100];
    int i=0;
    printf("Enter a sentence: ");
    do {
        scanf("%c",&x[i]);
        i++;
    } while (x[i-1]!='.');
    coutCharchetrs(x);
    return 0;
}

void coutCharchetrs(char x[]) {
    char y;
    int i=0,j=0;
    for(i=0;x[i]!='.';i++) {
        if(x[i]!='*') {
            y=x[i];
            int counter=0;
            for (j=0;x[j]!='.';j++) {
                if (y==x[j]) {
                    counter++;
                    x[j]='*';
                }
            }
            printf("There are %d %c in the sentence\n",counter,y);
        }
    }
}