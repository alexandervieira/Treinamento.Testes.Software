using AVS.NerdStore.Core.DomainObjects;

namespace AVS.NerdStore.Catalogo.Domain;

public class Categoria: BaseEntity
{
    public string Nome { get; private set; }
    public int Codigo { get; private set; }
    // EF Relation
    public ICollection<Produto> Produtos { get; set; }
    protected Categoria() { }
    public Categoria(string nome, int codigo)
    {
        Nome = nome;
        Codigo = codigo;
    }
    public override string ToString()
    {
        return $"{Nome} - {Codigo}";
    }
}
