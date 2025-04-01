#include <stdio.h>
/*
Program iki kare matrisin çarpımı için geliştirilmiştir. Gerekli küçük değişiklikler yapılarak
herhangi iki kare matrisi çarpabilir.
*/
void multpMatrix(int matrix1[3][3],int matrix2[3][3]);
int main() {
    int matrix1[3][3]={ {1,2,3},
                        {9,8,17},
                        {10,15,16}};

    int matrix2[3][3]={ {2,4,6},
                        {5,8,13},
                        {6,25,33}};

    multpMatrix(matrix1,matrix2);

}

void multpMatrix(int matrix1[3][3],int matrix2[3][3]) {
    int resultMatrix[3][3];
    int sum=0;
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            for (int k = 0; k < 3; k++) {
                sum = sum + matrix1[i][k] * matrix2[k][j];
            }
            resultMatrix[i][j] = sum;
            sum = 0;
        }
    }
    for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
            printf("%d\n",resultMatrix[i][j]);
        }
    }

}