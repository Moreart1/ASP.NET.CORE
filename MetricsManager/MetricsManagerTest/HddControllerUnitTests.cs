﻿using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MetricsManagerTest
{
    public class HddControllerUnitTests
    {
        private readonly HddMetricsController _controller;

        public HddControllerUnitTests()
        {
            _controller = new HddMetricsController();
        }


        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            var agentId = 1;

            var result = _controller.GetMetricsFromAgent(agentId);

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }


        [Fact]
        public void GetMetricsFromAllCluster_ReturnsOk()
        {
            var result = _controller.GetMetricsFromAllCluster();

            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}
