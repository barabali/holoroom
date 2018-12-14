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

## Beüzemelés

A Hololens emulátorhoz a telepítése előtt engedélyezni kell a Windows Developer üzemmódot, ezt több lépésnek is az előfeltétele, így érdemes az elején megtenni. Ezen kívül a Hyper-V engedélyezése is szükséges, amennyiben nincsen, az alaplap BIOS-ában a virtualizációs és ehhez hasonlóakat is be kell kapcsolni. 

Vigyázat, a VMWare virtualizációs szoftverrel egyes beállítások nem kompatibilisak, így lehet hogy csak az egyiket lehet futtatni azonos beállítások mellett.

A Visual Studiohoz az UWP fordításához szükséges kiegészítőn kívül fel kell tenni a Windows 10 SDK for UWP csomag megfelelő verzióját is, ami megegyezik a Hololens emulátor által elvárt verzióval.

A program futtatásához a Unity 2017.2-es verziója ajánlott, újabb kiadásokban a Prefab-ok széteshetnek, és a GameObject-ek nem találnak egymásra a szkriptekkel.

Az exportáláshoz a Unityben az *UWP SDK*-t kell telepíteni illetve a *.NET vagy IL2CPP scripting backend* -et, hogy le tudja build-elni az alkalmazást. A kiexportált *solution*t Visual Studioval megnyitva, majd a futtatási helyet *Remote Device*-ra állítva kell indítani.

Ekkor felajánlja a csatlakozási lehetőséget a Hololenshez. Fontos ezért, hogy tudjuk a Hololens IP címét, ami nem lehet NAT-olt hálózat mögött, vagy meg kell oldani a Port Forwardingot. Ez természtesen másik irányban is igaz, bár nem kötelező, csak azokhoz az alkalmazásokhoz amik kapcsolódnak a géphez. Figyelni kell a tűzfalra is, mert az alkalmazásban alapértelmezetten megadott 11000 portot blokkolja.

Amennyiben eléri a megadott IP címet, párosítás szükséges, a Hollens által kiírt számot kell a gépen is beírni. 

Minden futtatáskor a Hololensnek aktívnak kell lennie.

A Hololens adatait elérhetjük böngészőn keresztül is az IP címét megadva. Itt külön felhasználó létrehozása szükséges, amit a https://<YOUR_HOLOLENS_IP_ADDRESS>/devicesecurity.htm linken lehet megtenni.

Amennyiben a projekt importálása nem működik, a HoloToolkit-et hozzá lehet adni kézzel is a Unityhez az Import Asset funkcióval, azonban a felhasznált Prefab-ok beállításait ilyenkor más úton kell megszerezni.

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
