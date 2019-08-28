using System;
using System.Collections.Generic;
using Elders.Cronus;

namespace EntityFramework
{
    public class Root : AggregateRoot<RootState>
    {
        private Root() { }

        public string Key { get => state.Id.ToString(); set { } } // This is here only to satisfy EF
        public string Value => state.Value;

        public Root(RootId id)
        {
            state.Id = id ?? throw new ArgumentNullException(nameof(id));
            state.Value = "initial value";
        }

        public void ChangeValue(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));

            if (state.Value == value)
                return;

            if (state.History.Contains(value))
                return;

            state.History.Add(state.Value);
            state.Value = value;
        }
    }

    public class RootState : AggregateRootState<Root, RootId>
    {
        public RootState()
        {
            History = new List<string>();
        }

        public override RootId Id { get; set; }

        public string Value { get; set; }

        public IList<string> History { get; set; }
    }

    public class RootId : StringId
    {
        public RootId(StringId id) : base(id, "root") { }
        public RootId(string id) : base(id, "root") { }
    }
}
