using System.Xml;
using System.Xml.Serialization;
using System.Collections;

public class Config
{

    //Constructor
    public Config()
    {
        juego = "";
    }

    public Config(string name)
    {
        juego = name;
    }

    //Metodo de inclusion de imagenes y 
    [XmlAttribute]
    public string juego;


}
