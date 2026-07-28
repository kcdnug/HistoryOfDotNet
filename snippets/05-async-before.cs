client.GetStringAsync(url)
    .ContinueWith(t =>
    {
        if (t.IsFaulted)
            Log(t.Exception);
        else
            UpdateUi(t.Result);
    }, TaskScheduler.FromCurrentSynchronizationContext());
