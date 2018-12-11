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

**Play Space Manager**: A Unity Inspector-ban állítható paraméterek:

*Scan time*: A szkennelési sűrűség, ezt érdemes nem túl alacsony értékre állítani, különben nem tudja elég gyorsan végezni a Hololens a feldolgozás, de túl nagy sem lehet, mert akkor meg sokat kell várni a gépen az adatokra. 
Az observert indítja és állítja le, hogy ne menjen párhuzamosan a falak keresésével.
(Eredetileg a kezdeti, egyszeri szkennelés idejéért volt felelős, de ezt megszüntettem)

*Limit scanning by time*: A szkennelést nem végzi folyamatosan, csak egyszer, ezt azonban kikommenteltem, jelenleg nincs használva az a kódrészlet, a szkennelési idő másra használt már.

*Default Material*: Alap material ami szkennelés közben aktív

*Secondary Material*: A szkennelés végeztével ezt a Materialt kapják a falak. A SpatialMappingManager-ben ki van kapcsolva a megjelenítés így ez nem látszik.

*MinimumFoors, MinimumWalls*: Nem használtak, objektumok lehelyezésére voltak a mintában.

**SurfaceToPlanes**: Csak a Hololensen hívom meg, a rögzített környezeti adatokból készített poligonhálót kéri el és elemzi, hogy melyek a falak. A *Min Area*val állítható, hogy mekkora területtől számítson valami egybefüggő objektumnak
A falakat úgy keresi, hogy ellenőrzi a normálvektorokat, és ha sok nagyjából azonos irányba néz, akkor az fal.

Beállítható, hogy milyen típusú talált objektumokat tartson meg és töröljön (csak a falakat tartja meg jelenleg, kisebb és ismeretleneket töröl)

**Remove Surface Vertices**: Működését nem elemeztem, eltávolítja a felesleges objektumokat a modellből.
