list1 = [[[" "], [" "], [" "]],
         [[" "], [" "], [" "]],
         [[" "], [" "], [" "]],]
listxy=[]
def yazdirma():
    print("")
    print("-----------------")
    print(list1[0][0], list1[0][1], list1[0][2])
    print(list1[1][0], list1[1][1], list1[1][2])
    print(list1[2][0], list1[2][1], list1[2][2])
    print("-----------------")
    print("")
oyundurumu=True
oyundurumu2=True
sayaco1=0
sayaco2=0
while oyundurumu:
    yazdirma()
    eklenecekdeger=0
    deger=int(input("Hangi sütuna X eklemek istiyorsunuz: "))
    deger2=int(input("Hangi satıra X eklemek istiyorsunuz: "))
    while deger<=0 or deger2<=0 or deger>3 or deger2>3:
        print("Lütfen uygun bir konum giriniz.")
        deger = int(input("Hangi sütuna X eklemek istiyorsunuz: "))
        deger2 = int(input("Hangi satıra X eklemek istiyorsunuz: "))
    eklenecekdeger=deger2+deger*10
    while eklenecekdeger in listxy:
        print("Burası dolu!")
        yazdirma()
        deger = int(input("Hangi sütuna X eklemek istiyorsunuz: "))
        deger2 = int(input("Hangi satıra X eklemek istiyorsunuz: "))
        eklenecekdeger = deger2 + deger * 10
    list1[deger2 - 1][deger - 1].remove(" ")
    list1[deger2 - 1][deger - 1].append("X")
    sayaco1=sayaco1+1
    listxy.append(eklenecekdeger)
    yazdirma()
    for j in range(0,3):
        sayac1 = 0
        for b in range(0,3):
            if list1[j][b] == ["X"]:
                sayac1=sayac1+1
            if sayac1==3:
                print("1. Oyuncu oyunu kazandı!")
                oyundurumu=False
            elif sayaco1+sayaco2==9:
                oyundurumu2 = False
        sayac3 = 0
        for k in range(0,3):
            if list1[k][j] == ["X"]:
                sayac3=sayac3+1
                if sayac3==3:
                    print("1. Oyuncu kazandı!")
                    oyundurumu=False
                elif sayaco1 + sayaco2 == 9:
                    oyundurumu2 = False
    if not oyundurumu:
        break
    if not oyundurumu2:
        print("Oyunun kazananı olmadı.")
        break
    sayac5=0
    for i,j in zip(range(0,3),range(0,3)):
        if list1[i][j]==["X"]:
            sayac5=sayac5+1
            if sayac5==3:
                print("1. Oyuncu kazandı!")
                oyundurumu=False
            elif sayaco1+sayaco2==9:
                oyundurumu2 = False
    if not oyundurumu:
        break
    if not oyundurumu2:
        print("Oyunun kazananı olmadı.")
        break
    sayac7=0
    for i, j in zip(range(0, 3), range(2, -1, -1)):
        if list1[i][j]==["X"]:
            sayac7=sayac7+1
            if sayac7==3:
                print("1. Oyuncu kazandı!")
                oyundurumu=False
            elif sayaco1+sayaco2==9:
                oyundurumu2 = False
    if not oyundurumu:
        break
    if not oyundurumu2:
        print("Oyunun kazananı olmadı.")
        break
    deger3 = int(input("Hangi sütuna O eklemek istiyorsunuz: "))
    deger4 = int(input("Hangi satıra O eklemek istiyorsunuz: "))
    while deger3 <= 0 or deger4 <= 0 or deger3 > 3 or deger4 > 3:
        deger3 = int(input("Hangi sütuna O eklemek istiyorsunuz: "))
        deger4 = int(input("Hangi satıra O eklemek istiyorsunuz: "))
    eklenecekdeger=deger4+deger3*10
    while eklenecekdeger in listxy:
        print("Burası dolu!")
        yazdirma()
        deger3 = int(input("Hangi sütuna O eklemek istiyorsunuz: "))
        deger4 = int(input("Hangi satıra O eklemek istiyorsunuz: "))
        print(listxy)
        eklenecekdeger = deger4 + deger3 * 10
    list1[deger4 - 1][deger3 - 1].remove(" ")
    list1[deger4 - 1][deger3 - 1].append("O")
    sayaco2=sayaco2+1
    listxy.append(eklenecekdeger)
    yazdirma()
    for j in range(0, 3):
        sayac2 = 0
        for c in range(0, 3):
            if list1[j][c] == ["O"]:
                sayac2 = sayac2 +1
            if sayac2 == 3:
                print("2. Oyuncu oyunu kazandı!")
                oyundurumu=False
            elif sayaco1+sayaco2==9:
                oyundurumu2 = False
        if not oyundurumu:
            break
        if not oyundurumu2:
            print("Oyunun kazananı olmadı.")
            break
        sayac4 = 0
        for k in range(0,3):
            if list1[k][j] == ["O"]:
                sayac4=sayac4+1
                if sayac4==3:
                    print("2. Oyuncu kazandı!")
                    oyundurumu=False
                elif sayaco1 + sayaco2 == 9:
                    oyundurumu2 = False
    if not oyundurumu:
        break
    if not oyundurumu2:
        print("Oyunun kazananı olmadı.")
        break
    sayac6 = 0
    for i, j in zip(range(0, 3), range(0, 3)):
        if list1[i][j] == ["O"]:
            sayac6 = sayac6 + 1
            if sayac6 == 3:
                print("2. Oyuncu kazandı!")
                oyundurumu=False
            elif sayaco1+sayaco2==9:
                oyundurumu2 = False
    if not oyundurumu:
        break
    if not oyundurumu2:
        print("Oyunun kazananı olmadı.")
        break
    sayac8=0
    for i, j in zip(range(0, 3), range(2, -1, -1)):
        if list1[i][j]==["O"]:
            sayac8=sayac8+1
            if sayac8==3:
                oyundurumu=False
            elif sayaco1+sayaco2==9:
                oyundurumu2 = False
    if not oyundurumu:
        break
    if not oyundurumu2:
        print("Oyunun kazananı olmadı.")
        break
print("")
print("Toplam yapılan hamle sayısı;\nBirinci oyuncu: {}\nİkinci oyuncu: {}".format(sayaco1, sayaco2))