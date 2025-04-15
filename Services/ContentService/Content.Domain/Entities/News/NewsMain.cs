

using Content.Domain.Entities.Base;

namespace Content.Domain.Entities.News
{
    public class NewsMain : BaseEntity
    {
        public string NewsText { get; set; }

        public string NewsTitle { get; set; }
    }

}