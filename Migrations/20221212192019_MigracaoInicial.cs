using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ganrielapi.Migrations
{
    public partial class MigracaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Editoras",
                columns: table => new
                {
                    EditoraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEditora = table.Column<string>(type: "varchar(60)", nullable: false),
                    DescEditora = table.Column<string>(type: "varchar(150)", nullable: true),
                    CnpjEditora = table.Column<string>(type: "char(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoras", x => x.EditoraId);
                });

            migrationBuilder.CreateTable(
                name: "Estoques",
                columns: table => new
                {
                    EstoqueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QtdLivroEstoque = table.Column<int>(type: "int", nullable: false),
                    NomeEstoque = table.Column<string>(type: "varchar(60)", nullable: false),
                    DescEstoque = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoques", x => x.EstoqueId);
                });

            migrationBuilder.CreateTable(
                name: "StatusLivros",
                columns: table => new
                {
                    StatusLivroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescStatusLivroId = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusLivros", x => x.StatusLivroId);
                });

            migrationBuilder.CreateTable(
                name: "TiposPessoas",
                columns: table => new
                {
                    TipoPessoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescTipoPessoa = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPessoas", x => x.TipoPessoaId);
                });

            migrationBuilder.CreateTable(
                name: "TiposTelefones",
                columns: table => new
                {
                    TipoTelefoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescTipoTelefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposTelefones", x => x.TipoTelefoneId);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    LivroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeLivro = table.Column<string>(type: "varchar(60)", nullable: false),
                    DescLivro = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    StatusLivroId = table.Column<int>(type: "int", nullable: false),
                    EditoraId = table.Column<int>(type: "int", nullable: false),
                    QtdeLivro = table.Column<int>(type: "int", nullable: false),
                    EstoqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.LivroId);
                    table.ForeignKey(
                        name: "FK_Livros_Editoras_EditoraId",
                        column: x => x.EditoraId,
                        principalTable: "Editoras",
                        principalColumn: "EditoraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livros_Estoques_EstoqueId",
                        column: x => x.EstoqueId,
                        principalTable: "Estoques",
                        principalColumn: "EstoqueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livros_StatusLivros_StatusLivroId",
                        column: x => x.StatusLivroId,
                        principalTable: "StatusLivros",
                        principalColumn: "StatusLivroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    PessoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomePessoa = table.Column<string>(type: "varchar(60)", nullable: false),
                    SobrenomePessoa = table.Column<string>(type: "varchar(60)", nullable: false),
                    CpfPessoa = table.Column<string>(type: "char(11)", nullable: false),
                    DtNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoPessoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.PessoaId);
                    table.ForeignKey(
                        name: "FK_Pessoas_TiposPessoas_TipoPessoaId",
                        column: x => x.TipoPessoaId,
                        principalTable: "TiposPessoas",
                        principalColumn: "TipoPessoaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContatosPessoas",
                columns: table => new
                {
                    ContatoPessoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroTelefone = table.Column<string>(type: "varchar(20)", nullable: false),
                    EnderecoEmail = table.Column<string>(type: "varchar(150)", nullable: true),
                    DddTelefone = table.Column<int>(type: "int", nullable: false),
                    TipoTelefoneId = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContatosPessoas", x => x.ContatoPessoaId);
                    table.ForeignKey(
                        name: "FK_ContatosPessoas_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContatosPessoas_TiposTelefones_TipoTelefoneId",
                        column: x => x.TipoTelefoneId,
                        principalTable: "TiposTelefones",
                        principalColumn: "TipoTelefoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnderecosPessoas",
                columns: table => new
                {
                    EnderecoPessoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnderecoForn = table.Column<string>(type: "varchar(100)", nullable: false),
                    NumeroEndereco = table.Column<string>(type: "varchar(10)", nullable: false),
                    ComplementoEndereco = table.Column<string>(type: "varchar(20)", nullable: true),
                    BairroEndereco = table.Column<string>(type: "varchar(40)", nullable: false),
                    CidadeEndereco = table.Column<string>(type: "varchar(50)", nullable: false),
                    CepEndereo = table.Column<string>(type: "varchar(13)", nullable: false),
                    UfEndereco = table.Column<string>(type: "char(2)", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnderecosPessoas", x => x.EnderecoPessoaId);
                    table.ForeignKey(
                        name: "FK_EnderecosPessoas_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivrosPessoas",
                columns: table => new
                {
                    LivroPessoaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivroId = table.Column<int>(type: "int", nullable: false),
                    PessoaId = table.Column<int>(type: "int", nullable: false),
                    StatusAssociacao = table.Column<int>(type: "int", nullable: false),
                    QtdeEmprestada = table.Column<int>(type: "int", nullable: false),
                    DataDevolucao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivrosPessoas", x => x.LivroPessoaId);
                    table.ForeignKey(
                        name: "FK_LivrosPessoas_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "LivroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivrosPessoas_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estoques",
                columns: new[] { "EstoqueId", "DescEstoque", "NomeEstoque", "QtdLivroEstoque" },
                values: new object[] { 1, "Estoque de livros padrão", "Estoque de livros", 0 });

            migrationBuilder.InsertData(
                table: "StatusLivros",
                columns: new[] { "StatusLivroId", "DescStatusLivroId" },
                values: new object[,]
                {
                    { 1, "Default" },
                    { 2, "Ativo" },
                    { 3, "Indisponível" }
                });

            migrationBuilder.InsertData(
                table: "TiposPessoas",
                columns: new[] { "TipoPessoaId", "DescTipoPessoa" },
                values: new object[,]
                {
                    { 1, "Default" },
                    { 2, "Author" },
                    { 3, "Cliente" }
                });

            migrationBuilder.InsertData(
                table: "TiposTelefones",
                columns: new[] { "TipoTelefoneId", "DescTipoTelefone" },
                values: new object[,]
                {
                    { 1, "Celular" },
                    { 2, "Residencial" },
                    { 3, "Comercial" },
                    { 4, "Recado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContatosPessoas_PessoaId",
                table: "ContatosPessoas",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContatosPessoas_TipoTelefoneId",
                table: "ContatosPessoas",
                column: "TipoTelefoneId");

            migrationBuilder.CreateIndex(
                name: "IX_EnderecosPessoas_PessoaId",
                table: "EnderecosPessoas",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_EditoraId",
                table: "Livros",
                column: "EditoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_EstoqueId",
                table: "Livros",
                column: "EstoqueId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_StatusLivroId",
                table: "Livros",
                column: "StatusLivroId");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosPessoas_LivroId",
                table: "LivrosPessoas",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosPessoas_PessoaId",
                table: "LivrosPessoas",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_TipoPessoaId",
                table: "Pessoas",
                column: "TipoPessoaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContatosPessoas");

            migrationBuilder.DropTable(
                name: "EnderecosPessoas");

            migrationBuilder.DropTable(
                name: "LivrosPessoas");

            migrationBuilder.DropTable(
                name: "TiposTelefones");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Editoras");

            migrationBuilder.DropTable(
                name: "Estoques");

            migrationBuilder.DropTable(
                name: "StatusLivros");

            migrationBuilder.DropTable(
                name: "TiposPessoas");
        }
    }
}
