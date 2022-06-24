﻿using Arbus.Network.Application;
using Arbus.Network.Application.Exceptions;

namespace Arbus.Network.UnitTests.Application;

public class DefaultHttpClientTests : TestFixture
{
    [Test]
    public void EnsureNoTimeout_CancellationRequested_ThrowsHttpTimeoutException()
    {
        var canceallationTokenSource = new CancellationTokenSource();
        canceallationTokenSource.Cancel();

        Assert.Throws<HttpTimeoutException>(() => DefaultHttpClient.EnsureNoTimeout(canceallationTokenSource));
    }

    [Test]
    public void EnsureNoTimeout_CancellationNotRequested_NoException()
    {
        var canceallationTokenSource = new CancellationTokenSource();

        Assert.DoesNotThrow(() => DefaultHttpClient.EnsureNoTimeout(canceallationTokenSource));
    }

    [Test]
    public void EnsureNetworkAvailable_NetworkNotAvailable_ThrowsNoNetworkConnectionAvailableException()
    {
        var mockNetworkManager = CreateMock<INetworkManager>();
        mockNetworkManager.SetupGet(x => x.IsNetworkAvailable).Returns(false);

        DefaultHttpClient defaultHttpClient = new(default!, default!, default!);

        Assert.Throws<NoNetoworkConnectionException>(() => defaultHttpClient.EnsureNetworkAvailable());
    }

    [Test]
    public void EnsureNetworkAvailable_NetworkAvailable_NoException()
    {
        var mockNetworkManager = CreateMock<INetworkManager>();
        mockNetworkManager.SetupGet(x => x.IsNetworkAvailable).Returns(true);

        DefaultHttpClient defaultHttpClient = new(default!, default!, default!);

        Assert.DoesNotThrow(() => defaultHttpClient.EnsureNetworkAvailable());
    }
}