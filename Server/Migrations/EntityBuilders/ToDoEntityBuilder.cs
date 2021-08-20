using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace Daniel.ToDo.Migrations.EntityBuilders
{
    public class ToDoEntityBuilder : AuditableBaseEntityBuilder<ToDoEntityBuilder>
    {
        private const string _entityTableName = "DanielToDo";
        private readonly PrimaryKey<ToDoEntityBuilder> _primaryKey = new("PK_DanielToDo", x => x.ToDoId);
        private readonly ForeignKey<ToDoEntityBuilder> _moduleForeignKey = new("FK_DanielToDo_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public ToDoEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override ToDoEntityBuilder BuildTable(ColumnsBuilder table)
        {
            ToDoId = AddAutoIncrementColumn(table,"ToDoId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Name = AddMaxStringColumn(table,"Name");
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> ToDoId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Name { get; set; }
    }
}
