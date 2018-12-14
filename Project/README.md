# Részletek

[Scenes](https://github.com/barabali/holoroom/blob/master/Project/README.md#scenes)

[Scene files](https://github.com/barabali/holoroom/blob/master/Project/README.md#scene-files)

  - [Main camera](https://github.com/barabali/holoroom/blob/master/Project/README.md#main-camera)
  - [Cursor](https://github.com/barabali/holoroom/blob/master/Project/README.md#cursor)
  - [SpatialMapping](https://github.com/barabali/holoroom/blob/master/Project/README.md#spatialmapping)
  - [SpatialProcessing](https://github.com/barabali/holoroom/blob/master/Project/README.md#spatialprocessing)
  - [RemoteMapping](https://github.com/barabali/holoroom/blob/master/Project/README.md#remotemapping)
  - [Rocket Poster](https://github.com/barabali/holoroom/blob/master/Project/README.md#rocket-poster)

## Scenes

**WallDetector**

Ez az egyetlen scene található a projekt fő mappájában, ezen kívül a HoloToolkit mappában van(nak) csak példa Scene-ek amelyek a toolkit részét képezik.

Figyelni kell az egyes szkriptek namespace értékére és a megfelelő többi szkript ha nem azonos namespace, akkor az importra.

## Scene files

A létrehozott objektumok, szkriptek és beillesztett *Prefab*ek egy része a letöltött HoloToolkit-re hivatkozik, másik része az oktatóvideók alapján készített és letöltött fájlokra.

![Objects](https://github.com/barabali/holoroom/blob/master/images/objects.PNG)

Az objektumokban használt szkriptek használnak másik objektumon álló szkriptet, ez általában Singleton osztály példányának lekérését és használatát jelenti (pl Observer-t mindenki eléri a mapping-ban)

![Objects](https://github.com/barabali/holoroom/blob/master/images/architecture.PNG)

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

**ObjectSurfaceObserver:** Ha Unity editorban fut a program, akkor ellenőrzi, hogy van-e szobának megfelelő fájl megadva. Ha igen, akkor beállítja ezt a SpatialMappingManager adatforrásának. Ekkor a berakott fájlon ellenőrzi a falakat a Unityben. A támogatott fájltípus az obj, ezt exportál pl a DevicePortal a Hololensről is.

A következőt lehet látni gépen történő elemzéskor, vagy a Hololensen lehetne, amennyiben engedélyezve enne az létrehozott elemek megjelenítése:

![Processed](https://github.com/barabali/holoroom/blob/master/images/hololensen.PNG)

### SpatialProcessing

Itt történik a falak keresése és azoknak megfelelő 3D objektumok létrehozása. A tutorial alapján van felépítve kisebb módosításokkal, a Holotoolkit-nek is vannak hasonló osztályai és szkripjei több funkcióra, de azokra nem volt szükség.

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

### RemoteMapping

A távoli elérésért felel egyrészt a szerver oldalon, másrészt Hololens oldalon a csatlakozásért és adatküldésért. Az Prefab a Holotoolki SpatialMapping mappájából való, ami elő volt készítve a kapcsolódásra és kapcsolat fogadására

**Remote Mesh Target**: A szerver oldalért fele, ez nyújtja a portot a Hololens felé, és fogadja a Datastream-et, majd hozza létre belőle a 3D-s objektumot. Amint egy adatátvitel véget ért és lebontotta a kapcsolatot felkészül a következőre.

**Remote Mesh Source**: A Hololensen futó rész. Kapcsolódik a egadott ip címhez és porthoz, és van egy metódusa, amibe egy byte tömböt paraméterül adva elküldi a Unitynek.

**Remote Mapping Manager**: Ez az osztály volt felelős a példában az adatok küldéséért hang parancsra, de nem ezt használtam.

**File Surface Observer**: A hálózaton kereszült kapott modell mentéséért és újra betöltéséért felel, a megadott gombokra lépnek akcióba a funkciók. 

### Rocket Poster

Egy szkriptje van, ezen kívül lehelyezi a világban a felhasználóval szemben.

**SendMeshes**: A kikommentezett kód újra aktiválásával megadható egy időköz, aminek elteltével elküldi a jelenlegi feldolgozott falakat. Ekkor azonban konkurencia problémák léphetnek fel, mivel ezzel párhuzamosan fut az elemző folyamatok, amik az új falak létrehozásakor törlik az előzőeket, hogy ne legyen duplázódás, így az elküldéskor törlés alatt lévő falat is próbálhat elküldeni.

A jelenlegi módszerrel feliratkozik SurfaceMeshesToPlanes.Instance.MakePlanesComplete eseményre. Ez megoldja az előbbi problémát, mivel akkor végzi a küldést amikor kész a fal készítés, így nem törlésük alatt küldi.

A feliratkozott metódus a SendMeshes. Ez a függvény csak Hololensen fut le, Unityben nincs hatása. 

A függvény elkéri a SurfaceMeshesToPlanes-től a jelenlegi falakat. Ezeket a Unity-s *GameObject*eket ezután feldolgozom. Egy for ciklus lekéri mindegyiknek a *Mesh*-ét, ennek vektoriait leklónozza, majd egy tömbbe helyezi. Ez a Mesh osztály szerializálható, ezt teszi a végén, és a kapott byte tömböt küldi el a RemoteMeshSource SendData függvényével.
