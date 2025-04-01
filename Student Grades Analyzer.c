#include <stdio.h>

int main() {
    int numStudents;
    char names[100][50];
    int grades[100];

    printf("Enter the number of students: ");
    scanf("%d", &numStudents);

    for (int i = 1; i <= numStudents; i++) {
        printf("Enter name of student %d: ", i);
        scanf("%s", names[i]);
        printf("Enter the grade for stundent %s: ", names[i]);
        scanf("%d", &grades[i]);
    }
    for (int i = 1; i <= numStudents; i++) {
        printf("The grades for student %s is %d \n", names[i], grades[i]);
    }
    float avarage1=0;
    int highest=grades[0],counter=0;
    float sum=0;
    for (int i = 1; i <= numStudents; i++) {
        sum+=grades[i];
        if (grades[i]>highest) {
            counter=i;
            highest=grades[i];
        }
    }
    avarage1=sum/numStudents;
    for (int i = 1; i <= numStudents; i++) {
        if (grades[i]<avarage1) {
            printf("The grade of stundet %s is less than average\n", names[i]);
        }
    }
    printf("The sum grade is %.f\n", sum);
    printf("The average grade is %.2f\n", avarage1);
    printf("The highest grade is %d\n", highest);
    printf("%s got the highest grade, which is %d\n",names[counter], highest);
}