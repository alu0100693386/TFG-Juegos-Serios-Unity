using System.Xml;
using System.Xml.Serialization;


public class Personaje  
{
    /// <summary>
    /// Contructor por defecto, elimina errores en la serializacion.
    /// </summary>
    public Personaje()
    {
        Name = "NoName";
        Head = 1;
        Body = 1;
        Legs = 1;
    }
    /// <summary>
    /// Constructor de asignacion directa. Se utilizará preferiblemente este constructor. 
    /// </summary>
    public Personaje(string _name, int _head, int _body, int _legs)
    {
        Name = _name;
        Head = _head;
        Body = _body;
        Legs = _legs;
    }

    [XmlAttribute]
    public string Name {get; set;}
    [XmlAttribute]
    public int Head { get; set; }
    [XmlAttribute]
    public int Body { get; set; }
    [XmlAttribute]
    public int Legs { get; set; }
}
