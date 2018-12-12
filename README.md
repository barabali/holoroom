# WallDetector áttekintés

## Feladat:

Hololens segítségével a szobáról alkotott kép feldolgozása, a falak, nagyobb tárgyak jelzése és az ezekből létrehozott 3D-s térkép PC-re mentése.

## Felkészülés

A Unity megismerésével kezdtem a feladatot, készítettem a Unity oldalán található oktatóvideók alapján egy kisebb játékot.
Az utolsó anyagrészt önálló munkaként próbáltam megoldani, azaz megnéztem a feladatot és a kész megoldás játékmenetét, majd reprodukálni próbáltam ezt. Ebbe tartozott például az aszteroidák formájának (a felasznált 3D modell) váltakozása, az ellenséges űrhajók és azoknak mozgása illetve lövései.

<img src="https://github.com/barabali/holoroom/blob/master/images/shooter2d.PNG" width="40%">

Mivel ez 2D-s játékmenet volt, csak x,y koordinátákkal foglalkozott így étalakítottam a játékot 3D-sre. A hátteret és a részecskerendszereket kellett úgy forgatni, hogy továbbra is látványos maradjon. A játékot lehet követni a Unity editorban is, és lehet figyelni hogy pontosan mi történik az objektumokkal a kamerából kiúszás után.

<img src="https://github.com/barabali/holoroom/blob/master/images/shooter3d.PNG" width="40%"> <img src="https://github.com/barabali/holoroom/blob/master/images/shootereditor.PNG" width="50%">

https://unity3d.com/learn/tutorials/s/space-shooter-tutorial

## Megvalósítás

A Hololensen és gépen futtatható program Unity-vel készült, majd UWP alkalmazásként exportálva telepíthető a Holelens-re Microsoft Visual Studio segítségével.
A Unity-ben történő 3D kezeléshez szükséges a Holotoolkit/MixedRealityToolkit importálása, ami tartalmazza a Hololens eszközeinek alapszintű használatát, többek között az infra szenzorok segítségével létrehozott poligonháló feldolgozását, vagy a szkennelt objektumok felismeréséért felelős programkódokat.

Mixed Reality Toolkit: : https://github.com/Microsoft/MixedRealityToolkit-Unity

Microsoft Visual Studio: https://visualstudio.microsoft.com/vs/community/

Az alkalmazás futtatható Hololens emulátorral is, ekkor a beépített minta szobák közül választhatunk, amin a valós eszközhöz hasonlóan megkeresi a falakat és elküldi az alaprajzot a gépnek.
Visual Studio Community változata is elég, de az UWP kiegészítőt és a Hololens emulátort le kell tölteni hozzá.

## Futtatás

A projektet Unity-ben megnyitva, és a megfelelő kiegészítőket telepíteni (UWP exportáláshoz)
Be kell állítani a szerverként használt gép ip címét, amire a Hololens megkísérli küldeni az adatokat. Fontos, hogy a megadott port elérhető legyen, ne védje tűzfal, vagy egyéb szabály.

![Image of scene](https://github.com/barabali/holoroom/blob/master/images/selectfile.PNG)

![Image of_server](https://github.com/barabali/holoroom/blob/master/images/setip.PNG)

A projektet *File* menüpont alatt a *Build settings*-re kattintva egy új mappába mentjük.

![Image of layout](https://github.com/barabali/holoroom/blob/master/images/buildsettings.PNG)

Ezután a létrejött Visual Studio projetet megnyitva futtatható a Hololens-en a program a párosítás és egyebek után.

### Kép a kimentett alaprajzról:
![Image of layout](https://github.com/barabali/holoroom/blob/master/images/export.PNG)
