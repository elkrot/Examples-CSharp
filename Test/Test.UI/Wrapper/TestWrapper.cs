using System;
using System.Collections.Generic;
using Test.Model;

namespace Test.UI.Wrapper
{

    public class TestWrapper : ModelWrapper<TestEntity>
    {
        public TestWrapper(TestEntity model) : base(model)
        {
        }

        public int TestKey { get { return Model.TestKey; } }

        public string TestTitle
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int? FavoriteLanguageId
        {
            get { return GetValue<int?>(); }
            set {
                SetValue(value);
            }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
           
            switch (propertyName)
            {
                case nameof(TestTitle):
                    if (string.Equals(TestTitle, "Test", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "не может быть Test";
                    }
                    break;
                default:
                    yield return null;
                    break;
            }
        }
    }


}
