var app = angular.module('OperationApp', ['Authentication', 'datatables']);

app.filter('formatDurationTime', function () {
    return function (minutes) {
        var hours = Math.floor(minutes / 60);
        var remainingMinutes = minutes % 60;
        return hours + ' hour ' + remainingMinutes + ' minutes';
    };
});
app.filter('customDate', function ($filter) {
    return function (input) {
        if (!input) {
            return 'N/A';
        }
        var timestamp = input.replace('/Date(', '').replace(')/', '');
        var date = new Date(parseInt(timestamp));
        return $filter('date')(date, 'EEEE, MMMM d, yyyy');
    };
});

app.service('crudReportServices', ['$http', 'Upload', function ($http, Upload) {

    this.TotalRevenue = function () {
        return $http({
            method: 'GET',
            url: '/Admin/Reports/TotalRevenue',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

    this.TotalRetriveRevenue = function () {
        return $http({
            method: 'GET',
            url: '/Admin/Reports/TotalRetriveRevenue',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };


    this.GetRevenueReport = function (dataObject) {
        return $http({
            method: 'POST',
            url: '/Admin/Reports/GetRevenueReport',
            data: { report: dataObject },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (result) {
            return result.data;
        });
    }

    this.GetGrantChartForDriver = function () {
        return $http({
            method: 'GET',
            url: '/Admin/Reports/GetGrantChartForDriver',
            headers: { 'content-type': 'application/json' }
        }).then(function (response) {
            return response.data;
        });
    };

}]);



app.controller('RevenueController', function ($http, $scope, $timeout, LogoutServices, crudReportServices, $window, crudPropService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.propertyDisable = true;
        $scope.rangewise = ''; // Initialize with an empty string or null
        $('#revenueDetails').hide();
        var flatpickrInstance = $("#kt_datepicker_7").flatpickr({
            altInput: true,
            altFormat: "F j, Y",
            dateFormat: "Y-m-d",
            mode: "range",
            onChange: function (selectedDates, dateStr, instance) {
                // Use $timeout to update the model safely
                $timeout(function () {
                    $scope.rangewise = dateStr; // Update the range string
                });
            }
            //onChange: function (selectedDates, dateStr, instance) {
            //    // Update the AngularJS model manually
            //    angular.element($("#kt_datepicker_7")).scope().$apply(function ($scope) {
            //        $scope.rangewise = dateStr; // Update the range string
            //    });
            //}
        });


        crudPropService.GetPropertyAreaDropDown().then(function (result) {

            if (result == "Exception") {
            }
            else {
                $scope.AreaDropdown = result;

            }
        });

        $scope.GetPropertybyArea = function () {
            if ($scope.ddlArea != null) {
                crudPropService.GetPropertyByAreaDropDown($scope.ddlArea).then(function (result) {

                    if (result == "Exception") {
                    }
                    else {
                        $scope.PropertyDropdown = result;
                        $scope.propertyDisable = false;

                    }
                });
            }
        }

        crudReportServices.TotalRevenue().then(function (result) {

            if (result != "Exception") {
                $scope.TotalRevenue = result;

            }
        });

        crudReportServices.TotalRetriveRevenue().then(function (result) {

            if (result != "Exception") {
                $scope.TotalRetreiveRevenue = result;

            }
        });

      



        $scope.msgVRange = "field is required";
        $scope.msgVArea = "field is required";
        $scope.msgVProperty = "field is required";
        $scope.FilterData = function (isvalid) {

            $scope.RevnueReport = [];
            $scope.TowerDetails = [];
            if (isvalid) {
                $('#btnsearch').hide();
                $('#btnloader').show();
                var dates = $scope.rangewise.split(" to ");
                var startDate = dates[0];
                var endDate = dates[1] ? dates[1] : null; // Check if the end date exists

                var details = {};
                details.StartDate = startDate;
                details.EndDate = endDate;
                details.propaID = $scope.ddlArea;
                details.vID = $scope.ddlProperty;
                crudReportServices.GetRevenueReport(details).then(function (result) {

                    $('#btnsearch').show();
                    $('#btnloader').hide();
                    $('#revenueDetails').show();
                    if (result != "Exception") {

                        $scope.RevnueReport = result;
                        $scope.TowerDetails = result.Towers;
                        // Create separate arrays
                        const towerNames = $scope.TowerDetails.map(tower => tower.TowerName);
                        const amounts = $scope.TowerDetails.map(tower => tower.Amount === null ? 0 : tower.Amount);
                        // Initialize the charts on load
                        initCharts(towerNames, amounts);
                        if ($scope.TowerDetails.length !== 0) {
                            $('#tbl_packageslist').show();
                            $('#tbl_dummypacakges').hide();
                            for (var i = 0; i <= $scope.TowerDetails.length - 1; i++) {
                                $scope.TowerDetails[i].index = i + 1;
                            }
                            $scope.RevenueTList = $scope.TowerDetails;
                        }
                        else if ($scope.TowerDetails.length === 0) {
                            $('#tbl_packageslist').hide();
                            $('#tbl_dummypacakges').show();
                            $('#spanLoader').hide();
                            $('#spanEmptyRecords').show();
                        }
                    }
                });


            }
        }

        $scope.resetfields = function () {
            $scope.ddlArea = null;
            var $dltAreaID = $('#dltAreaID');
            $dltAreaID.val(null).trigger('change.select2');
            $scope.ddlProperty = null;
            var $dltPropertyID = $('#dltPropertyID');
            $dltPropertyID.val(null).trigger('change.select2');
            //$scope.msgVRange = "";
            //$scope.msgVArea = "";
            //$scope.msgVProperty = "";
            $scope.rangewise = ''; // Clear the AngularJS model
            flatpickrInstance.clear(); // Clear the selected dates in Flatpickr
            $scope.SearchForm.$setPristine(); // Reset form
            $scope.SearchForm.$setUntouched(); // Reset form
        }




        // Initialize a variable to hold the current chart instance
        let towerRevenueChart;

        function initCharts(towers, amounts) {
            // Clear the previous chart if it exists
            if (towerRevenueChart) {
                towerRevenueChart.destroy();
            }

            // Get colors for the number of towers
            const colors = generateColors(towers.length);

            // Get the context for the chart
            var ctx2 = document.getElementById('towerRevenueChart').getContext('2d');

            // Create a new chart
            towerRevenueChart = new Chart(ctx2, {
                type: 'bar',
                data: {
                    labels: towers,
                    datasets: [{
                        label: 'Revenue',
                        data: amounts,
                        backgroundColor: colors.backgroundColors,
                        borderColor: colors.borderColors,
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        y: { beginAtZero: true }
                    }
                }
            });
        }


        // Generate random colors for each tower
        function generateColors(count) {
            const backgroundColors = [];
            const borderColors = [];

            for (let i = 0; i < count; i++) {
                const r = Math.floor(Math.random() * 255);
                const g = Math.floor(Math.random() * 255);
                const b = Math.floor(Math.random() * 255);

                backgroundColors.push(`rgba(${r}, ${g}, ${b}, 0.2)`);
                borderColors.push(`rgba(${r}, ${g}, ${b}, 1)`);
            }

            return { backgroundColors, borderColors };
        }

        $scope.exportData = function (file_name, output_type, data) {
            if (output_type == "xlsx") {
                // Use COALESCE to replace null values in the Amount field with 0
                alasql('SELECT [index] as S_No,[TowerName], COALESCE([Amount], 0) AS Amount INTO XLSX("' + file_name + '",{headers:true}) FROM ?', [data]);
                file_name = file_name + ".xlsx";
            }
            else {
                file_name = file_name + ".csv";
                // Same COALESCE logic applied here for CSV export
                alasql('SELECT [index] as S_No,[TowerName], COALESCE([Amount], 0) AS Amount INTO CSV("' + file_name + '",{headers:true}) FROM ?', [data]);
            }
        }

    }
});


app.controller('GantChartController', function ($http, $scope, $timeout, LogoutServices, crudReportServices, $window, crudPropService) {
    var Auth = LogoutServices.getValue();
    if (Auth == null || Auth == false) {
        $window.location.href = "/Account/Index";
    }
    else {
        $scope.GetGantChart = function () {
            $('#grantchar').hide();
            $('#spinnerdiv').show();
            crudReportServices.GetGrantChartForDriver().then(function (result) {

                if (result != "Exception") {
                    //$scope.GrandChart = result;

                    if (result.length != 0) {
                        const data = result;
                        $('#grantchar').show();
                        $('#spinnerdiv').hide();
                        //var data = [
                        //    {
                        //        "TeamName": "Team 1",
                        //        "AreaBased": []
                        //    },
                        //    {
                        //        "TeamName": "Team 2",
                        //        "AreaBased": []
                        //    },
                        //    {
                        //        "TeamName": "Team3",
                        //        "AreaBased": []
                        //    }
                        //];
                        const width = 900, height = data.length * 100;
                        const svg = d3.select("#ganttChart").attr("width", width).attr("height", height);

                        // Time scale set to start at 08:00 and end at 17:00
                        const yScale = d3.scaleBand()
                            .domain(data.map(d => d.Team))  // Use Team to map the y-axis
                            .range([50, height - 50])
                            .padding(0.2);

                        const timeScale = d3.scaleLinear()
                            .domain([8 * 60, 17 * 60])  // 08:00 to 17:00
                            .range([150, width - 50]);

                        // Adjust the x-axis tick format to ensure it shows full hours
                        const xAxis = d3.axisTop(timeScale)
                            .tickValues(d3.range(8 * 60, 17 * 60 + 1, 60))  // Every hour from 08:00 to 17:00
                            .tickFormat(d => {
                                const hour = Math.floor(d / 60);
                                const minute = d % 60;
                                return `${hour}:${minute < 10 ? '0' + minute : minute}`;  // Format minutes as "00"
                            });

                        svg.append("g")
                            .attr("transform", "translate(0, 40)")
                            .call(xAxis);

                        // Set up the y-axis to display the team names
                        const yAxis = d3.axisLeft(yScale);

                        // Position the y-axis properly to display the team names aligned with the task bars
                        svg.append("g")
                            .attr("transform", "translate(150, 0)")
                            .call(yAxis);

                        // Tooltip for hovering over tasks and free spaces
                        const tooltip = d3.select("#tooltip");

                        function showTooltip(content, event) {
                            tooltip.html(content)
                                .style("left", `${event.pageX + 15}px`)
                                .style("top", `${event.pageY}px`)
                                .classed("show", true);
                        }

                        function hideTooltip() {
                            tooltip.classed("show", false);
                        }

                        function renderTask(start, end, team, areaName, service) {
                            const barWidth = timeScale(end) - timeScale(start);

                            svg.append("rect")
                                .attr("x", timeScale(start))
                                .attr("y", yScale(team))
                                .attr("width", barWidth)
                                .attr("height", 30)
                                .attr("class", "bar")
                                .on("mouseover", (event) => showTooltip(`Task in ${areaName} from ${formatTime(start)} to ${formatTime(end)}`, event))
                                .on("mousemove", (event) => tooltip.style("left", `${event.pageX + 15}px`).style("top", `${event.pageY}px`))
                                .on("mouseout", hideTooltip)
                                .on("click", () => showModal(team, areaName, service, formatTime(start), formatTime(end)));
                        }

                        function showModal(team, areaName, service, startTime, endTime) {

                            document.getElementById("modalTeamName").textContent = team;
                            document.getElementById("modalAreaName").textContent = areaName;
                            document.getElementById("modalService").textContent = service;
                            document.getElementById("modalStartTime").textContent = startTime;
                            document.getElementById("modalEndTime").textContent = endTime;

                            const taskModal = new bootstrap.Modal(document.getElementById('taskModal'));
                            taskModal.show();
                        }

                        function renderFreeSpace(start, end, y, className) {
                            const barWidth = timeScale(end) - timeScale(start);

                            svg.append("rect")
                                .attr("x", timeScale(start))
                                .attr("y", y)
                                .attr("width", barWidth)
                                .attr("height", 30)
                                .attr("class", `free-slot ${className}`)
                                /*  .on("mouseover", (event) => showTooltip("Free Slot: Click to assign a task", event))*/
                                .on("mousemove", (event) => tooltip.style("left", `${event.pageX + 15}px`).style("top", `${event.pageY}px`))
                                .on("mouseout", hideTooltip)
                            /*   .on("click", () => alert("Assign a new task here!"));*/
                        }

                        function formatTime(minutes) {
                            const hour = Math.floor(minutes / 60);
                            const min = minutes % 60;
                            return `${hour}:${min < 10 ? '0' + min : min}`;
                        }

                        function compareTeamsAndRender() {
                            data.forEach(team => {
                                // Check if AreaBased array is empty
                                if (!Array.isArray(team.AreaBased) || team.AreaBased.length === 0) {
                                    // Render full free space from 08:00 to 17:00
                                    renderFreeSpace(8 * 60, 17 * 60, yScale(team.Team), "light-grey");
                                    return; // Skip further processing for this team
                                }

                                // Flatten the tasks for the team from AreaBased
                                const tasks = team.AreaBased.map(area => {
                                    const start = area.Time.Start.Hours * 60 + area.Time.Start.Minutes;
                                    const end = area.Time.End.Hours * 60 + area.Time.End.Minutes;
                                    return { start, end, areaName: area.Area, service: area.Service };
                                });

                                //const tasks = team.AreaBased.flatMap(area => area.Time.map(time => {
                                //    const start = time.Start.Hours * 60 + time.Start.Minutes;
                                //    const end = time.End.Hours * 60 + time.End.Minutes;
                                //    return { start, end, areaName: area.AreaName, service: area.Service };
                                //}));

                                // Sort tasks by their start time
                                tasks.sort((a, b) => a.start - b.start);

                                // Free space before the first task
                                if (tasks[0].start > 8 * 60) {
                                    renderFreeSpace(8 * 60, tasks[0].start, yScale(team.Team), "light-grey");
                                }

                                // Render tasks and gaps between them
                                tasks.forEach((task, i) => {
                                    renderTask(task.start, task.end, team.Team, task.areaName, task.service);

                                    // Render free space between tasks
                                    if (i < tasks.length - 1) {
                                        const className = tasks[i].areaName !== tasks[i + 1].areaName ? "red" : "light-grey";
                                        renderFreeSpace(task.end, tasks[i + 1].start, yScale(team.Team), className);
                                    }
                                });

                                // Free space after the last task
                                if (tasks[tasks.length - 1].end < 17 * 60) {
                                    renderFreeSpace(tasks[tasks.length - 1].end, 17 * 60, yScale(team.Team), "light-grey");
                                }
                            });
                        }

                        compareTeamsAndRender();



                    }




                }
            });
        }
    }
});


app.controller('TeamController', function ($scope) {
    // Dummy data for teams, rosters, and towers
    $scope.teams = [
        { number: 1, cleaners: ['John', 'Doe'], contact: '123-456-789' },
        { number: 2, cleaners: ['Jane', 'Smith'], contact: '987-654-321' }
    ];

    $scope.towers = ['Tower 1', 'Tower 2', 'Tower 3'];

    $scope.rosters = [
        { team: 1, serviceTime: '08:00 - 10:00', propertyCode: 'P123', area: 'Area A', subArea: 'SubArea 1', isLocationChange: false, nextLocation: 'Same' },
        { team: 2, serviceTime: '10:00 - 12:00', propertyCode: 'P456', area: 'Area B', subArea: 'SubArea 2', isLocationChange: true, nextLocation: 'New Location' }
    ];

    $scope.filteredRoster = $scope.rosters;

    // Function to filter roster based on selected filters
    $scope.filterRoster = function () {
        $scope.filteredRoster = $scope.rosters.filter(function (roster) {
            return (!$scope.filterDate || roster.date === $scope.filterDate) &&
                (!$scope.filterTeam || roster.team == $scope.filterTeam) &&
                (!$scope.filterTower || roster.tower == $scope.filterTower) &&
                (!$scope.filterTime || roster.serviceTime.startsWith($scope.filterTime));
        });
    };

    // Dummy function to notify team
    $scope.notifyTeam = function (team) {
        alert('Notification sent to Team ' + team.number);
    };

    // Dummy function to notify driver
    $scope.notifyDriver = function () {
        alert('Driver notified with location: ' + $scope.driverLocation);
    };

    // Send bulk notifications to selected cleaners/teams
    $scope.sendBulkNotifications = function () {
        alert('Notifications sent to: ' + $scope.selectedCleanersTeams.join(', '));
    };

    // List of all cleaners/teams for multi-select dropdown
    $scope.allCleanersTeams = ['John', 'Doe', 'Jane', 'Smith', 'Team 1', 'Team 2'];
});

app.controller('LayoutController', function ($scope, $window, crudUserService, LogoutServices, $http) {

    crudUserService.GetProfilePic().then(function (result) {

        $('#myprofile').show();
        $('#mainProfile').show();
        if (result != '' && result != null) {
            if (result.Value != null) {
                $scope.profilePic = result.Value;

            }
            else {
                $scope.profilePic = "../../Images/DefaultUser.png";
            }

        }
        else {
            $scope.profilePic = "../../Images/DefaultUser.png";
        }
    });

    crudUserService.GetUpdateUserDetails().then(function (result) {

        if (result == "Exception") {

        }
        else {
            $scope.userDetails = result;
            $scope.CleanerName = result.Name;
        }

    });

    $scope.Logout = function () {

        $http({
            method: 'POST',
            url: '/Staff/Dashboard/LogOut',
            dataType: 'JSON',
            headers: { 'content-type': 'application/json' }
        }).then(function (result) {

            if (result.data == "SUCCESS") {
                LogoutServices.setValue(false);
                $window.location.href = '/Account/Index'
            }
            else if (result.data == "Exception") {
                toastr.warning('Something went wrong, please try again later', { title: 'Warning!' });
            }
        });
    }

});




app.controller('ReportController', function ($scope) {

    // Initialize chart data and render charts with static data
    $scope.initCharts = function () {

        // Function to generate dynamic colors for the chart
        function generateColors(count) {
            const backgroundColors = [];
            const borderColors = [];

            for (let i = 0; i < count; i++) {
                const r = Math.floor(Math.random() * 255);
                const g = Math.floor(Math.random() * 255);
                const b = Math.floor(Math.random() * 255);

                backgroundColors.push(`rgba(${r}, ${g}, ${b}, 0.5)`);
                borderColors.push(`rgba(${r}, ${g}, ${b}, 1)`);
            }

            return { backgroundColors, borderColors };
        }

        // Generate colors for the number of towers
        const colors = generateColors(8);


        // Chart 1: Active Teams/Cleaners per Area/Tower
        const teamsByAreaData = {
            labels: ['Tower A', 'Tower B', 'Tower C', 'Tower D', 'Tower E', 'Tower F', 'Tower G', 'Tower H'],
            datasets: [{
                data: [10, 15, 8, 12, 10, 15, 8, 12],
                backgroundColor: colors.backgroundColors,
                borderColor: colors.borderColors,
                borderWidth: 1
            }]
        };
        new Chart(document.getElementById('teamsByAreaChart'), {
            type: 'doughnut',
            data: teamsByAreaData,
            options: {
                responsive: true,
                plugins: { legend: { position: 'top' } }
            }
        });



        // Chart 2: Teams/Cleaners by Service Vertical
        const serviceVerticalData = {
            labels: ['Regular', 'Deep', 'Specialized', 'Car Wash'],
            datasets: [{
                label: 'Teams',
                data: [20, 18, 12, 8],
                backgroundColor: '#1cc88a'
            }]
        };
        console.log(serviceVerticalData);
        new Chart(document.getElementById('serviceVerticalChart'), {
            type: 'bar',
            data: serviceVerticalData,
            options: {
                responsive: true,
                plugins: { legend: { display: false } },
                scales: { y: { beginAtZero: true } }
            }
        });

        // Chart 3: Efficiency - Services per Team
        const efficiencyData = {
            labels: ['Team 1', 'Team 2', 'Team 3', 'Team 4'],
            datasets: [{
                label: 'Services',
                data: [5, 8, 7, 6],
                backgroundColor: '#36b9cc'
            }]
        };
        new Chart(document.getElementById('efficiencyChart'), {
            type: 'line',
            data: efficiencyData,
            options: {
                responsive: true,
                plugins: { legend: { position: 'top' } },
                scales: { y: { beginAtZero: true } }
            }
        });

        // Chart 4: Average Rating of Teams
        const ratingData = {
            labels: ['Team 1', 'Team 2', 'Team 3', 'Team 4'],
            datasets: [{
                label: 'Average Rating',
                data: [4.5, 3.8, 4.2, 4.0],
                backgroundColor: '#f6c23e'
            }]
        };
        new Chart(document.getElementById('ratingChart'), {
            type: 'bar',
            data: ratingData,
            options: {
                responsive: true,
                plugins: { legend: { display: false } },
                scales: { y: { beginAtZero: true, max: 5 } }
            }
        });
    };

    // Call initCharts function on load
    $scope.initCharts();
});