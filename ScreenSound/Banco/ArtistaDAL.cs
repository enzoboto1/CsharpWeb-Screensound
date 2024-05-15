using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Banco
{
    internal class ArtistaDAL
    {
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();
            using var connection = new Connection().ObterConexao();
            connection.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand command = new SqlCommand(sql, connection);
            using SqlDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                string nomeArtista = Convert.ToString(dataReader["Nome"]);
                string bioArtista = Convert.ToString(dataReader["Bio"]);
                int IdArtista = Convert.ToInt32(dataReader["id"]);
                Artista artista = new Artista(nomeArtista, bioArtista) { Id = IdArtista };

                lista.Add(artista);
            }
            return lista;
        }

        public void Adicionar(Artista artista)
        {
            using var connection = new Connection().ObterConexao();
            connection.Open();
            string sql = "INSERT INTO Artistas (Nome, FotoPerfil, Bio) VALUES (@nome, @perfilPadrao, @bio)";
            SqlCommand sqlCommand = new SqlCommand(sql, connection);

            sqlCommand.Parameters.AddWithValue("@nome", artista.Nome);
            sqlCommand.Parameters.AddWithValue("@perfilPadrao", artista.FotoPerfil);
            sqlCommand.Parameters.AddWithValue("@bio", artista.Bio);

            int retorno = sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
    }
}
