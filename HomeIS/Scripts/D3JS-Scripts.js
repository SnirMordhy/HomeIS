﻿$(document).ready(function () {
    (function interval() {
        $.ajax({
            dataType: "json",
            url: "/Apartments/AllApartmentsJSON",
            success: function (data) {
                update(data.length);
            }
        });
        $.ajax({
            dataType: "json",
            url: "/Apartments/ApartmentCountGroupJSON",
            success: function (data) {
                donut(data);
            }
        });
        setTimeout(interval,1000);
    })();
});

d3.select(self.frameElement).style("height", height + "px");

function donut(data) {

    var width = 960,
        height = 500,
        radius = Math.min(width, height) / 2;

    var color = d3.scale.ordinal()
        .range(["#98abc5", "#8a89a6", "#7b6888", "#6b486b", "#a05d56", "#d0743c", "#ff8c00"]);

    var arc = d3.svg.arc()
        .outerRadius(radius - 10)
        .innerRadius(radius - 70);

    var pie = d3.layout.pie()
        .sort(null)
        .value(function (d) { return d.Count; });

    $("#donut").children().remove();

    var svg = d3.select("#donut").append("svg")
        .attr("width", width)
        .attr("height", height)
        .append("g")
        .attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");
        data.forEach(function (d) {
            d.Count = +d.Count;
        });

        var g = svg.selectAll(".arc")
            .data(pie(data))
            .enter().append("g")
            .attr("class", "arc");

        g.append("path")
            .attr("d", arc)
            .style("fill", function (d) { return color(d.data.City); });

        g.append("text")
            .attr("transform", function (d) { return "translate(" + arc.centroid(d) + ")"; })
            .attr("dy", ".35em")
            .text(function (d) { return d.data.City; });
}