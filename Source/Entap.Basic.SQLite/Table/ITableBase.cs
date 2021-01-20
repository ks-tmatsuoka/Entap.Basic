using System;
namespace Entap.Basic.SQLite
{
    /// <summary>
    /// テーブルの規定インターフェース
    /// </summary>
    public interface ITableBase
    {
        /// <summary>
        /// Id
        /// </summary>
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
