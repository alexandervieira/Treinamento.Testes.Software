namespace AVS.TesteBasico
{
    public class Funcionario : Pesssoa
    {
        public double Salario { get; private set; }
        public NivelProfissional NivelProfissional { get; set; }
        public IList<string> Habilidades { get; private set; }
        public Funcionario(string nome, double salario)
        {
            if(string.IsNullOrEmpty(nome)) { throw new ArgumentNullException("Informe o nome do funcionário"); }
            Nome = nome;            
            DefinirSalario(salario);
            DefinirHabilidades();
        }

        private void DefinirHabilidades()
        {
            var habilidadesBasicas = new List<string>()
            {
                "Lógica de Programação",
                "OOP"
            };

            Habilidades = habilidadesBasicas;

            switch (NivelProfissional)
            {
                case NivelProfissional.Pleno:
                    Habilidades.Add("Testes de Software");
                    break;
                case NivelProfissional.Senior:
                    Habilidades.Add("Testes de Software");
                    Habilidades.Add("Microsserviços");
                    break;
            }
        }

        private void DefinirSalario(double salario)
        {
            if (salario < 500) throw new Exception("Salário inferior ao permitido");

            Salario = salario;
            if (salario < 2000) NivelProfissional = NivelProfissional.Junior;
            else if (salario >= 2000 && salario < 8000) NivelProfissional = NivelProfissional.Pleno;
            else if (salario >= 8000) NivelProfissional = NivelProfissional.Senior;
        }
    }
}
