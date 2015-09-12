using System.Xml;
using System.Xml.Serialization;
using System.Collections;

public class Game {

    //Constructor
    public Game()
    {
        nFases = 0;
        Fase = 0;
        nombre = "";
    }

    //Metodo de inclusion de imagenes y 
    [XmlAttribute]
    public string nombre; 
    [XmlAttribute]
    public int nFases; 
    [XmlAttribute]
    public int Fase; 
    [XmlAttribute]
    public string[] FotoUrl;
    [XmlAttribute]
    public string[] Clues;

}
