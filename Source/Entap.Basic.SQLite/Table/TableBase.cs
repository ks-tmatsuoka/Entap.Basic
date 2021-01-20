using System;
using SQLite;

namespace Entap.Basic.SQLite
{
    public class TableBase : ITableBase
    {
        public TableBase()
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int AppId { get; set; }

        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}
