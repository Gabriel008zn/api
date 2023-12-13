using Microsoft.AspNetCore.Mvc;
using gabrielapi.Models;

namespace gabrielapi.Ultils;

public class Verificar
{
    public bool VerificarEndereco(EnderecoPessoa endereco)
    {
        if ((endereco.EnderecoForn.Length <= 5) || (endereco.BairroEndereco.Length <= 5) ||
                (endereco.CidadeEndereco.Length <= 5))
            return false;
        if (endereco.NumeroEndereco.Length < 1)
            return false;
        if (endereco.CepEndereo.Length != 8)
            return false;
        if (endereco.UfEndereco.Length != 2)
            return false;

        return true;
    }

    //Inicio verificação cpf
    public bool VerificarPriDigito(char[] cpf)
    {
        var tools = new CalVerificador();
        var digitoVeri = new CalVerificador();

        int multi = 10;
        for (int i = 0; i < 9; i++)
        {
            tools.ResulMulti = multi * (int)char.GetNumericValue(cpf[i]);
            tools.Soma += tools.ResulMulti;
            --multi;
        }

        if (DigitoVerificador(tools.Soma) == (int)char.GetNumericValue(cpf[9]))
            return (true);
        else
            return (false);
    }

    public bool VerificarSegDigito(char[] cpf)
    {
        var tools = new CalVerificador();
        var digitoVeri = new CalVerificador();

        int multi = 11;
        for (int i = 0; i < 10; i++)
        {
            tools.ResulMulti = multi * (int)char.GetNumericValue(cpf[i]);
            tools.Soma += tools.ResulMulti;
            --multi;
        }

        if (DigitoVerificador(tools.Soma) == (int)char.GetNumericValue(cpf[10]))
            return (true);
        else
            return (false);
    }

    private int DigitoVerificador(int valoresSomados)
    {
        int resulId = 11 - (valoresSomados % 11);

        if (resulId >= 10)
            return (0);
        else
            return (resulId);

    }
    //Fim verificação cpf

}
