#include <stdio.h>
int main() {
    char characters[8]= {'A', 'h', 'm', 'e', 't', 'c', 'a','n'};
    int size=sizeof(characters)/sizeof(characters[0]);
    for (int i = 0; i < size+1; i++) {
        for (int j = i; j < size; j++) {
            printf("%c", characters[j]);
        }
        for (int j = 0; j < i; j++) {
            printf("%c",characters[j]);
        }
        printf("\n");
    }
    return 0;
}
