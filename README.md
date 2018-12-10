# Áttekintés

## Feladat:

Hololens segítségével a szobáról alkotott kép feldolgozása, a falak, nagyobb tárgyak jelzése és az ezekből létrehozott 3D-s térkép PC-re mentése.

## Megvalósítás

A Hololensen és gépen futtatható program Unity-vel készült, majd UWP alkalmazásként exportálva telepíthető a Holelens-re Microsoft Visual Studio segítségével.
A Unity-ben történő 3D kezeléshez szükséges a Holotoolkit/MixedRealityToolkit importálása, ami tartalmazza a Hololens eszközeinek alapszintű használatát, többek között az infra szenzorok segítségével létrehozott poligonháló feldolgozását, vagy a szkennelt objektumok felismeréséért felelős programkódokat.

Mixed Reality Toolkit: : https://github.com/Microsoft/MixedRealityToolkit-Unity
Visual Studio: https://visualstudio.microsoft.com/

Az alkalmazás futtatható Hololens emulátorral is, ekkor a beépített minta szobák közül választhatunk, amin a valós eszközhöz hasonlóan megkeresi a falakat és elküldi az alaprajzot a gépnek.
Visual Studio Community változata is elég, de az UWP kiegészítőt és a Hololens emulátort le kell tölteni hozzá.

