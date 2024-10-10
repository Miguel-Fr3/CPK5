using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CPK5.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_CP5_PACIENTE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DtNascimento = table.Column<DateTime>(type: "DATE", nullable: false),
                    CPF = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CP5_PACIENTE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_CP5_PLANO_SAUDE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NmPlano = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CdPlano = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Cobertura = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CP5_PLANO_SAUDE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_CP5_PACIENTE_PLANO",
                columns: table => new
                {
                    PacienteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PlanoSaudeId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_CP5_PACIENTE_PLANO", x => new { x.PacienteId, x.PlanoSaudeId });
                    table.ForeignKey(
                        name: "FK_TB_CP5_PACIENTE_PLANO_TB_CP5_PACIENTE_PacienteId",
                        column: x => x.PacienteId,
                        principalTable: "TB_CP5_PACIENTE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_CP5_PACIENTE_PLANO_TB_CP5_PLANO_SAUDE_PlanoSaudeId",
                        column: x => x.PlanoSaudeId,
                        principalTable: "TB_CP5_PLANO_SAUDE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TB_CP5_PACIENTE",
                columns: new[] { "Id", "CPF", "DtNascimento", "Endereco", "Nome", "Telefone" },
                values: new object[] { 1, "123456789", new DateTime(2024, 10, 10, 13, 44, 49, 912, DateTimeKind.Local).AddTicks(6417), "endereço inicial", "Paciente Inicial", "123456789" });

            migrationBuilder.InsertData(
                table: "TB_CP5_PLANO_SAUDE",
                columns: new[] { "Id", "CdPlano", "Cobertura", "NmPlano" },
                values: new object[] { 1, 1, "Cobertura Inicial", "Plano Inicial" });

            migrationBuilder.InsertData(
                table: "TB_CP5_PACIENTE_PLANO",
                columns: new[] { "PacienteId", "PlanoSaudeId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_TB_CP5_PACIENTE_PLANO_PlanoSaudeId",
                table: "TB_CP5_PACIENTE_PLANO",
                column: "PlanoSaudeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_CP5_PACIENTE_PLANO");

            migrationBuilder.DropTable(
                name: "TB_CP5_PACIENTE");

            migrationBuilder.DropTable(
                name: "TB_CP5_PLANO_SAUDE");
        }
    }
}
