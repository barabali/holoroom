# Részletek

## Scenes

**WallDetector**

Ez az egyetlen scene található a projekt fő mappájában, ezen kívül a HoloToolkit mappában van(nak) csak példa Scene-ek amelyek a toolkit részét képezik.

## Scene files

A létrehozott objektumok, szkriptek és beillesztett *Prefab*ek egy része a letöltött HoloToolkit-re hivatkozik, másik része az oktatóvideók alapján készített és letöltött fájlokra.

![Objects](https://github.com/barabali/holoroom/blob/master/images/objects.PNG)

### Main Camera
Nem történt benne változtatás, megfelel a SpatialMapping tutorialban találhatónak.

### Cursor
Ez is az alapértelmezett Cursor a Prefab-ek közül, gyakorlati haszna nincsen azon kívül hogy a képernyő közepét mutatja.

### SpatialMapping
Ez az osztály felelős az infra szenzorokkal kapott kép feldolgozásáért. Ebben az objektumban szerepelnek szkriptek az oktatóvideóból és a Holotoolkitből, attól függően hogy mennyi funkcióra volt szükség.

**SpatialMappingObserver:** Futtatja a *SurfaceObserver*t a paraméterként megadott időközökkel, és a szkennelt felületet ábrázoló poligonháló létrehozásáért és törléséért felelős.
A Triangles Per Cubic Meter gyakorlatilag a 3D-s objektum "felbontását", részletességét állítja. Mivel falak ellenőrzésére használtam, így ez viszonylag alacsony, de nem lehet túl kevés, különben nem fog elég normálvektor egy irányba nézni, hogy falnak érzékelje.

**SpatialMappingManager:** Ő indítja az obervert. Ezen kívül a szkennelt objektumok kirajzolásáért felelős. Ennek a Material-ja a testre szabható *Surface Material*, amit Wireframe-nek választottam.
Az alkalmazásban nem szükséges a szkenneléshez és alaprajz küldéshez ezeknek az adatoknak a megjelenítése, ezért a *Draw Visual Meshes* kapcsoló kikapcsolva marad.

**ObjectSurfaceObserver:** Ha Unity editorban fut a program, akkor ellenőrzi, hogy van-e szobának megfelelő fájl megadva. Ha igen, akkor beállítja ezt a SpatialMappingManager adatforrásának.

### SpatialProcessing

Itt történik a falak keresése és azoknak megfelelő 3D objektumok létrehozása. 
