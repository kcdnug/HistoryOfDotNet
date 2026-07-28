try
{
    string json = await client.GetStringAsync(url);
    UpdateUi(json);
}
catch (HttpRequestException ex)
{
    Log(ex);
}
