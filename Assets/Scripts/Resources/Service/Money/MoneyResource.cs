public class MoneyResource : BaseResource
{
    public MoneySettings MoneySettings => _settings as MoneySettings;

    public MoneyResource(MoneySettings settings, ResourceFactory resourceFactory)
    {
        _settings = settings;
        _resourceFactory = resourceFactory;
    }

    public override void Collect()
    {
        if (MoneySettings.Count == 100)
        {
            UpdateMoneyType();
        }
        base.Collect();
    }

    private void UpdateMoneyType()
    {
        switch (MoneySettings.MoneyType)
        {
            case MoneySettings.Type.Bronze:
                MoneySettings.MoneyType = MoneySettings.Type.Silver;
                break;
            case MoneySettings.Type.Silver:
                MoneySettings.MoneyType = MoneySettings.Type.Gold;
                break;
            case MoneySettings.Type.Gold:
                MoneySettings.MoneyType = MoneySettings.Type.Platinum;
                break;
            case MoneySettings.Type.Platinum:
                break;
        }
        MoneySettings.Count = 1;
    }
}