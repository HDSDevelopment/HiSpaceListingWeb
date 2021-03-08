////investor calculation
//function returnCalculation() {
//	var rtnHoldingPeriod = $('#rtn_period').val();
//	var rtnDiscount = $('#rtn_discount').val();
//	var rtnInflation = $('#rtn_inflation').val();
//	var rtnNOI = $('#rtn_noi').val();
//	var rtnEstimatedCap = $('#rtn_cap').val();
//	var discountFactor = [];
//	var inflationRate = [];
//	var NOI = [];
//	var capitalExpenses = [];
//	var netCashFlow = [];
//	var holdingPeriodYears = [];
//	//console.log(rtnHoldingPeriod);
//	//console.log(rtnDiscount);
//	//console.log(rtnInflation);
//	//console.log(rtnNOI);
//	//console.log(rtnEstimatedCap);
//	//discount factor
//	for (i = 1; i <= rtnHoldingPeriod; i++) {
//		discountFactor.push(parseFloat(Math.pow((1 + ((rtnDiscount) / 100)), -i).toFixed(2)));
//	}
//	//holding periods list array
//	for (i = 1; i <= rtnHoldingPeriod; i++) {
//		holdingPeriodYears.push("Year " + i);
//	}
//	console.log(holdingPeriodYears);
//	//inflation rate
//	for (i = 1; i <= rtnHoldingPeriod; i++) {
//		if (i == 1) {
//			var ir = 1;
//			inflationRate.push(ir);
//		} else {
//			var ir = parseFloat(((ir) * (1 + (rtnInflation) / 100)).toFixed(8));
//			//console.log(ir);
//			inflationRate.push(ir);
//		}
//	}
//	//console.log(inflationRate);
//	//console.log(inflationRate.length);

//	//NOI calculation
//	for (i = 0; i < inflationRate.length; i++) {
//		//console.log(inflationRate[i]);
//		//NOI.push(parseFloat((inflationRate[i] * rtnNOI).toFixed(4)));
//		NOI.push(parseInt(inflationRate[i] * rtnNOI));
//	}
//	console.log(NOI);
//	//Capital Expenses
//	for (i = 1; i <= rtnHoldingPeriod; i++) {
//		//capitalExpenses.push(parseFloat(rtnEstimatedCap / rtnHoldingPeriod).toFixed(4));
//		capitalExpenses.push(parseInt(rtnEstimatedCap / rtnHoldingPeriod));
//	}
//	console.log(capitalExpenses);
//	//Net Cash Flow
//	for (i = 0; i < rtnHoldingPeriod; i++) {
//		//console.log('test');
//		//netCashFlow.push(parseFloat(NOI[i] - capitalExpenses[i]).toFixed(4));
//		netCashFlow.push(parseInt(NOI[i] - capitalExpenses[i]));
//	}
//	console.log(netCashFlow);
//	//chart cash flow
//	rtnCashFlow(NOI, capitalExpenses, netCashFlow, holdingPeriodYears);
//	//table append
//	rtnCashFlowTable(NOI, capitalExpenses, netCashFlow, holdingPeriodYears);
//};
//$.cookie("visits", 10);
//console.log($.cookie("visits"));
//$.removeCookie("visits");

//console.log($.cookie("visits"));

//interest rate show
var rtnLtvInput = $('#rtn_ltv');
$(rtnLtvInput).keyup(function () {
	//console.log(rtnLtvInput.val());
	if (rtnLtvInput.val() != 0 && rtnLtvInput.val() != 0.00 && rtnLtvInput.val() != "") {
		$('.intrest-rate').removeClass('d-none');
	} else {
		$('.intrest-rate').addClass('d-none');
	}
});
//exit method show
var rtnExitMethod = $('#rtn_exitmethod');
$(rtnExitMethod).change(function () {
	//console.log(rtnExitMethod.val());
	if (rtnExitMethod.val() == "DCF") {
		$('.exit-cap').addClass('d-none');
	} else {
		$('.exit-cap').removeClass('d-none');
	}
});


function returnCalculation(process) {

	if (process == 1) {
		//$('#SliderLTV').next().find('.jslider-value span').html(rtnLTV);
		//$('#SliderLTV').siblings('.current-value__slider').html('Current LTV "' + rtnLTV + '"');

		////console.log($('#SliderLTV').siblings('.current-value__slider').html());
		//$('#SliderDiscount').next().find('.jslider-value span').html(rtnDiscount);
		//$('#SliderDiscount').siblings('.current-value__slider').html('Current Discunt "' + rtnDiscount + '"');

		//$('#SliderCap').next().find('.jslider-value span').html(rtnExitcap);
		//$('#SliderCap').siblings('.current-value__slider').html('Current Exit Cap "' + rtnExitcap + '"');

		//$('#SliderTax').next().find('.jslider-value span').html(rtnTax);
		//$('#SliderTax').siblings('.current-value__slider').html('Current Tax "' + rtnTax + '"');
		console.log($('#SliderLTV').next().find('.jslider-value span').html().replace(/,/g, '.'));
		console.log($('#SliderDiscount').next().find('.jslider-value span').html().replace(/,/g, '.'));
		console.log($('#SliderCap').next().find('.jslider-value span').html().replace(/,/g, '.'));
		console.log($('#SliderTax').next().find('.jslider-value span').html().replace(/,/g, '.'));
	}
	var formData = new FormData();
	var rtnInvestment = $('#rtn_investment').val();
	var rtnNOI = $('#rtn_noi').val();
	
	var rtnInflation = $('#rtn_inflation').val();
	
	var rtnIntrest = $('#rtn_intrest').val();
	if (rtnIntrest == "") {
		rtnIntrest = 0.00;
	}
	var rtnPeriod = $('#rtn_period').val();
	var rtnExitmethod = $('#rtn_exitmethod').val();
	var rtnExitcap = $('#rtn_exitcap').val();
	if (rtnExitcap == "" || rtnExitcap == "0.00") {
		rtnExitcap = 1.00;
	}
	
	
	if (process == 1) {
		var rtnLTV = $('#SliderLTV').next().find('.jslider-value span').html().replace(/,/g, '.');
		var rtnDiscount = $('#SliderDiscount').next().find('.jslider-value span').html().replace(/,/g, '.');
		var rtnCap = $('#SliderCap').next().find('.jslider-value span').html().replace(/,/g, '.');
		var rtnTax = $('#SliderTax').next().find('.jslider-value span').html().replace(/,/g, '.');
	} else {
		var rtnLTV = $('#rtn_ltv').val();
		if (rtnLTV == "") {
			rtnLTV = 0.00;
		}
		var rtnCap = $('#rtn_cap').val();
		if (rtnCap == "") {
			rtnCap = 0.00;
		}
		var rtnDiscount = $('#rtn_discount').val();
		var rtnTax = $('#rtn_tax').val();
		if (rtnTax == "") {
			rtnTax = 0.00;
		}
	}
	


	//var rtnInvestment = 125000;
	//var rtnNOI = 9500;
	//var rtnCap = 15000;
	//var rtnInflation = 6.00;
	//var rtnLTV = 20.00;
	//var rtnIntrest = 0.00;
	//var rtnPeriod = 3;
	//var rtnExitmethod = "Direct cap";
	//var rtnExitcap = 1;
	//var rtnDiscount = 12.00;
	//var rtnTax = 25.35;
	var rtnAdditionalYears = 11;
	formData.append("Investment", rtnInvestment);
	formData.append("CurrentNOI", rtnNOI);
	formData.append("EstimatedCapExOrTIThroughHold", rtnCap);
	formData.append("Inflation", rtnInflation);
	formData.append("LoanToValue", rtnLTV);
	formData.append("InterestRate", rtnIntrest);
	formData.append("HoldingPeriodInYears", rtnPeriod);
	formData.append("ExitMethod", rtnExitmethod);
	formData.append("ExitCapRate", rtnExitcap);
	formData.append("DiscountRate", rtnDiscount);
	formData.append("TaxRate", rtnTax);
	formData.append("AdditionalYears", rtnAdditionalYears);

	for (var pair of formData.entries()) {
		console.log(pair[0] + ' - ' + pair[1]);
	}

	$("body").append("<div class='loader_new'></div>");

	$.ajax({
		type: "POST",
		url: "/Investor/GetReturnCalculation",
		data: formData,
		dataType: "html",
		processData: false,
		contentType: false,
		success: function (response) {
			//console.log(response);
			var obj = JSON.parse(response);
			console.log(obj);
			var NOI = [];
			var netCashFlow = [];
			var capitalExpenses = [];
			var holdingPeriodYears = [];
			var leveredCash = [];
			var unleveredCash = [];
			var postTaxCash = [];
			var irr = [];
			var profit = [];
			var multiple = [];
			var peakEquity = [];
			var operatingCashFlow = obj['operatingCashFlow'];
			if (operatingCashFlow == null || operatingCashFlow == "") {
				alert('Please enter the valid inputs...!');
				removeBodyLoader();
			}
			var responseNOI = obj['operatingCashFlow']['netOperatingIncomeList'];
			
			var responseNetCashFlow = obj['operatingCashFlow']['netCashFlowList'];
			var responseCapitalExpenses = obj['operatingCashFlow']['capitalExpenseList'];
			var responseLeveredCash = obj['financingCashFlow']['leveredCashFlow'];
			var responseUnleveredCash = obj['investmentCashFlow']['unleveredCashFlowValue'];
			var responsePostTaxCash = obj['taxCashFlow']['postTaxCashFlow'];
			var responseIrr = obj['irr'];
			var responseProfit = obj['profit'];
			var responseMultiple = obj['multiple'];
			var responsePeakEquity = obj['peakEquity'];

			//console.log(responseNOI);
			//console.log(rtnPeriod);
			//holding periods list array
			for (i = 0; i <= rtnPeriod; i++) {
				holdingPeriodYears.push("Year " + i);
			}
			$.each(responseNOI.slice(0, parseInt(rtnPeriod) + 1), function (i, element) {
				//console.log(element);
				//console.log(element['itemValue']);
				//console.log(element['forYear']);
				NOI.push(element['itemValue'].toFixed());
				console.log(NOI);
			}); 
			$.each(responseNetCashFlow.slice(0, parseInt(rtnPeriod) + 1), function (i, element) {
				netCashFlow.push(element['itemValue'].toFixed());
			}); 
			$.each(responseCapitalExpenses.slice(0, parseInt(rtnPeriod) + 1), function (i, element) {
				capitalExpenses.push(element['itemValue'].toFixed());
			});
			$.each(responseLeveredCash.slice(0, parseInt(rtnPeriod) + 1), function (i, element) {
				leveredCash.push(element['itemValue'].toFixed());
			});
			$.each(responseUnleveredCash.slice(0, parseInt(rtnPeriod) + 1), function (i, element) {
				unleveredCash.push(element['itemValue'].toFixed());
			});
			$.each(responsePostTaxCash.slice(0, parseInt(rtnPeriod) + 1), function (i, element) {
				postTaxCash.push(element['itemValue'].toFixed());
			});
			$.each(responseIrr, function (i, element) {
				irr.push(element.toFixed(2));
			});
			//console.log(irr);
			$.each(responseProfit, function (i, element) {
				profit.push(element.toFixed(2));
			});
			$.each(responseMultiple, function (i, element) {
				multiple.push(element.toFixed(2));
			});
			$.each(responsePeakEquity, function (i, element) {
				peakEquity.push(element.toFixed(2));
			});

			//var responseCurrentValues = [];
			//responseCurrentValues.push(
			//	{ 'NOI': NOI },
			//	{ 'netCashFlow': netCashFlow },
			//	{ 'capitalExpenses': capitalExpenses },
			//	{ 'holdingPeriodYears': holdingPeriodYears},
			//	{ 'leveredCash': leveredCash},
			//	{ 'unleveredCash': unleveredCash},
			//	{ 'postTaxCash': postTaxCash},
			//	{ 'irr': irr},
			//	{ 'profit': profit},
			//	{ 'multiple': multiple},
			//	{ 'peakEquity': peakEquity}
			//);
			var cookievalue;
			var responseCurrentValues = {
				'currentNOI': NOI,
				'currentnetCashFlow': netCashFlow,
				'currentcapitalExpenses': capitalExpenses,
				'currentholdingPeriodYears': holdingPeriodYears,
				'currentleveredCash': leveredCash,
				'currentunleveredCash': unleveredCash,
				'currentpostTaxCash': postTaxCash,
				'currentirr': irr,
				'currentprofit': profit,
				'currentmultiple': multiple,
				'currentpeakEquity': peakEquity
			};

			//chart cash flow
			rtnCashFlow(NOI, capitalExpenses, netCashFlow, holdingPeriodYears, leveredCash, unleveredCash, postTaxCash, rtnLTV, rtnTax);
			//table append
			rtnCashFlowTable(NOI, capitalExpenses, netCashFlow, holdingPeriodYears, leveredCash, unleveredCash, postTaxCash, rtnLTV, rtnTax);
			//all returns
			if (process == 0) {
				$.cookie("responseCurrentValues", JSON.stringify(responseCurrentValues));
				console.log(JSON.parse($.cookie("responseCurrentValues")));
				cookievalue = JSON.parse($.cookie("responseCurrentValues"));
				//console.log(cookievalue);
				//console.log(cookievalue['currentirr']);

				overAllReturns(cookievalue['currentirr'], cookievalue['currentprofit'], cookievalue['currentmultiple'], cookievalue['currentpeakEquity'], irr, profit, multiple, peakEquity, 0);
			} else {
				cookievalue = JSON.parse($.cookie("responseCurrentValues"));
				console.log(cookievalue);
				overAllReturns(cookievalue['currentirr'], cookievalue['currentprofit'], cookievalue['currentmultiple'], cookievalue['currentpeakEquity'], irr, profit, multiple, peakEquity, 1);
			}

			//slider section
			setTimeout(function () {
			$('.rtn-slider').removeClass('d-none');
			//console.log($('#SliderLTV').val());
			//$('#SliderLTV').val(rtnLTV);
			//console.log($('#SliderLTV').val());
			//$('#SliderDiscount').val(rtnDiscount);
			//$('#SliderCap').val(rtnExitcap);
			//$('#SliderTax').val(rtnTax);

			$('#SliderLTV').val(rtnLTV);
			console.log($('#SliderLTV').val());
			$('#SliderLTV').next().find('.jslider-value span').html(rtnLTV);
			$('#SliderLTV').siblings('.current-value__slider').html('Current LTV "' + rtnLTV + '"');

			//console.log($('#SliderLTV').siblings('.current-value__slider').html());
			$('#SliderDiscount').val(rtnDiscount);
			$('#SliderDiscount').next().find('.jslider-value span').html(rtnDiscount);
			$('#SliderDiscount').siblings('.current-value__slider').html('Current Discunt "' + rtnDiscount + '"');

			$('#SliderCap').val(rtnExitcap);
			$('#SliderCap').next().find('.jslider-value span').html(rtnExitcap);
			$('#SliderCap').siblings('.current-value__slider').html('Current Exit Cap "' + rtnExitcap + '"');

			$('#SliderTax').val(rtnTax);
			$('#SliderTax').next().find('.jslider-value span').html(rtnTax);
			$('#SliderTax').siblings('.current-value__slider').html('Current Tax "' + rtnTax + '"');

			
				jQuery("#SliderLTV").slider({from: 1,to: 100,step: .1,round: 1,format: {format: '##.0',locale: 'de'},dimension: ' %',skin: "round"});
				jQuery("#SliderDiscount").slider({ from: 1, to: 100, step: .1, round: 1, format: { format: '##.0', locale: 'de' }, dimension: ' %', skin: "round" });
				jQuery("#SliderCap").slider({ from: 1, to: 100, step: .1, round: 1, format: { format: '##.0', locale: 'de' }, dimension: ' %', skin: "round" });
				jQuery("#SliderTax").slider({ from: 1, to: 100, step: .1, round: 1, format: { format: '##.0', locale: 'de' }, dimension: ' %', skin: "round" });
			}, 1000);

			//console.log();
			removeBodyLoader();
			removeBlur();
		},
		error: function (response) {
			removeLoader();
			removeBodyLoader();
			alert("server not ready please try afterwards");
		}
	});
	
}

function removeBlur() {
	$('.chart-sample').remove();
	$('#container, #rtn-cashflow-table, #irr_chart, #profit_chart, #multiple_chart, #peak_chart').removeClass('div-blur');
};

function rtnCashFlow(NOI, capitalExpenses, netCashFlow, holdingPeriodYears, leveredCash, unleveredCash, postTaxCash, rtnLTV, rtnTax) {
	//console.log('testing array');
	var dataSeries = [];
	dataSeries.length = 0;
	//console.log(dataSeries.length);
	dataSeries = [
		{
			name: 'NOI',
			type: 'line',
			data: NOI
		},
		{
			name: 'Net Cash Flow',
			type: 'line',
			data: netCashFlow
		},
		//{
		//	name: 'Levered Cash Flow',
		//	type: 'line',
		//	data: leveredCash
		//},
		{
			name: 'Unlevered Cash Flow',
			type: 'line',
			data: unleveredCash
		}
		//,
		//{
		//	name: 'Post Tax Cash Flow',
		//	type: 'line',
		//	data: postTaxCash
		//}
	];

	//loan levered validation for graph
	if (rtnLTV != 0 && rtnLTV != 0.00 && rtnLTV != "") {
		dataSeries.push({
			name: 'Levered Cash Flow',
			type: 'line',
			data: leveredCash
		});
	}
	//tax validation for graph
	if (rtnTax != 0 && rtnTax != 0.00 && rtnTax != "") {
		dataSeries.push({
			name: 'Post Tax Cash Flow',
			type: 'line',
			data: postTaxCash
		});
	}


	console.log(dataSeries.length);
	console.log(dataSeries);
	//console.log('test')
	//console.log(NOI)
	//chart flow
	var dom = document.getElementById("container");
	var myChart = echarts.init(dom);
	var app = {};
	var option;
	option = {
		//title: {
		//	text: 'Cash Flow'
		//},
		renderer: 'svg',
		tooltip: {
			trigger: 'axis',
			axisPointer: {
				type: 'cross'
			},
			backgroundColor: 'rgba(255, 255, 255, 0.8)'
		},
		legend: {
			data: ['NOI', 'Net Cash Flow', 'Levered Cash Flow', 'Unlevered Cash Flow', 'Post Tax Cash Flow'],
			type: 'scroll',
			orient: 'horizontal'
		},
		grid: {
			show: false,
			left: "10%",
			top: "10%",
			width: "80%",
			height: "80%",
			containLabel: true
		},
		//toolbox: {
		//	feature: {
		//		saveAsImage: {}
		//	}
		//},
		xAxis: {
			type: 'category',
			boundaryGap: false,
			data: holdingPeriodYears,
			name: "No of Years",
			nameLocation: "middle",
			nameTextStyle: {
				fontWeight: "bold",
				padding: [10, 0, 0, 0]
			}
		},
		yAxis: [{
			type: "value",
			show: true,
			name: "Amount",
			nameLocation: "middle",
			nameTextStyle: {
				fontWeight: "bold",
				align: "center",
				padding: [4, 0, 0, 0]
			},
			boundaryGap: false,
			axisLine: {
				show: true
			}
		}],
		series: dataSeries
	};
	if (option && typeof option === 'object') {
		myChart.clear();
		myChart.setOption(option);
	}
};

function rtnCashFlowTable(NOI, capitalExpenses, netCashFlow, holdingPeriodYears, leveredCash, unleveredCash, postTaxCash, rtnLTV, rtnTax) {
	console.log(holdingPeriodYears);
	var cashTable = $("#rtn-cashflow-table");
	//heading
	cashTable.find('thead tr').empty();
	cashTable.find('thead tr').append('<th scope="col">Cash Flow</th>');
	$.each(holdingPeriodYears, function (i, element) {
		//console.log(element);
		//console.log(i);
		cashTable.find('thead tr').append('<th scope="col">' + element + '</th>');
	});

	//NOI	
	var cashNOI = $('#row-noi');
	cashNOI.empty();
	cashNOI.append('<th scope="col">NOI</th>');
	$.each(NOI, function (i, element) {
		cashNOI.append('<td>' + numberWithCommas(element) + '</td>');
	});

	//Capital Expenses	
	var cashCapital = $('#row-capital');
	cashCapital.empty();
	cashCapital.append('<th scope="col">Capital Expenses</th>');
	$.each(capitalExpenses, function (i, element) {
		cashCapital.append('<td>' + numberWithCommas(element) + '</td>');
	});

	//Net cash flow
	var cashNetcash = $('#row-netCash');
	cashNetcash.empty();
	cashNetcash.append('<th scope="col">Net Cash Flow</th>');
	$.each(netCashFlow, function (i, element) {
		cashNetcash.append('<td>' + numberWithCommas(element) + '</td>');
	});

	//UnLevered cash flow
	var cashUnlevered = $('#row-unLevered');
	cashUnlevered.empty();
	cashUnlevered.append('<th scope="col">Unlevered Cash Flow</th>');
	$.each(unleveredCash, function (i, element) {
		cashUnlevered.append('<td>' + numberWithCommas(element) + '</td>');
	});

	//loan levered validation for graph
	if (rtnLTV != 0 && rtnLTV != 0.00 && rtnLTV != "") {
		//Levered cash flow
		var cashLevered = $('#row-levered');
		cashLevered.empty();
		cashLevered.append('<th scope="col">Levered Cash Flow</th>');
		$.each(leveredCash, function (i, element) {
			cashLevered.append('<td>' + numberWithCommas(element) + '</td>');
		});
	} else {
		$('#row-levered').empty();
	}
	//tax validation for graph
	if (rtnTax != 0 && rtnTax != 0.00 && rtnTax != "") {
		//Post tax cash flow
		var cashPostTax = $('#row-postTax');
		cashPostTax.empty();
		cashPostTax.append('<th scope="col">Post Tax Cash Flow</th>');
		$.each(postTaxCash, function (i, element) {
			cashPostTax.append('<td>' + numberWithCommas(element) + '</td>');
		});
	} else {
		$('#row-postTax').empty();
	}
};

function overAllReturns(cirr, cprofit, cmultiple, cpeakEquity, irr, profit, multiple, peakEquity, process) {
	if (process == 1) {
		barWidth = '30%'
	} else {
		barWidth = '40%'
	}
	//IRR
	var irr_dom = document.getElementById("irr_chart");
	var irr_myChart = echarts.init(irr_dom);
	var irr_app = {};
	var irr_option;
	var irr_dataseries = [];
	
	irr_dataseries = [
		{
			name: 'Current IRR',
			type: 'bar',
			barWidth: barWidth,
			data: cirr,
			color: '#ff9900'
		}
	];
	if(process == 1) {
		irr_dataseries.push({
			name: 'Updated IRR',
			type: 'bar',
			barWidth: barWidth,
			data: irr,
			color: "#d2d2d2"
		});
	};
	irr_option = {
		renderer: 'svg',
		tooltip: {
			trigger: 'axis',
			axisPointer: {
				type: 'shadow'
			}
		},
		grid: {
			containLabel: true,
			left: "0",
			top: "5%"
		},
		xAxis: [
			{
				type: 'category',
				data: ['UnLevered', 'Levered', 'Post Tax'],
				axisTick: {
					alignWithLabel: true
				},
				axisLabel: {
					show: true,
					rotate: -30,
					margin: 10
				}
			}
		],
		yAxis: [
			{
				type: 'value',
				axisLine: {
					show: true
				}
			}
		],
		series: irr_dataseries
		
	};

	if (irr_option && typeof irr_option === 'object') {
		irr_myChart.clear();
		irr_myChart.setOption(irr_option);
	}

	//Profit
	var profit_dom = document.getElementById("profit_chart");
	var profit_myChart = echarts.init(profit_dom);
	var profit_app = {};
	var profit_option;
	var profit_dataseries = [];
	profit_dataseries = [
		{
			name: 'Current Profit',
			type: 'bar',
			barWidth: barWidth,
			data: cprofit,
			color: '#2196f3'
		}
	];
	if (process == 1) {
		profit_dataseries.push({
			name: 'Updated Profit',
			type: 'bar',
			barWidth: barWidth,
			data: profit,
			color: "#d2d2d2"
		});
	};
	profit_option = {
		renderer: 'svg',
		tooltip: {
			trigger: 'axis',
			axisPointer: {
				type: 'shadow'
			}
		},
		grid: {
			containLabel: true,
			left: "0",
			top: "5%"
		},
		xAxis: [
			{
				type: 'category',
				data: ['UnLevered', 'Levered', 'Post Tax'],
				axisTick: {
					alignWithLabel: true
				},
				axisLabel: {
					show: true,
					rotate: -30,
					margin: 10
				}
			}
		],
		yAxis: [
			{
				type: 'value',
				axisLine: {
					show: true
				}
			}
		],
		series: profit_dataseries 	
	};

	if (profit_option && typeof profit_option === 'object') {
		profit_myChart.clear();
		profit_myChart.setOption(profit_option);
	}

	//Multiple
	var multiple_dom = document.getElementById("multiple_chart");
	var multiple_myChart = echarts.init(multiple_dom);
	var multiple_app = {};
	var multiple_option;
	var multiple_dataseries = [];
	multiple_dataseries = [
		{
			name: 'Current Multiple',
			type: 'bar',
			barWidth: barWidth,
			data: cmultiple,
			color: '#E91E63'
		}
	];
	if (process == 1) {
		multiple_dataseries.push({
			name: 'Updated Multiple',
			type: 'bar',
			barWidth: barWidth,
			data: multiple,
			color: "#d2d2d2"
		});
	};
	multiple_option = {
		renderer: 'svg',
		tooltip: {
			trigger: 'axis',
			axisPointer: {
				type: 'shadow'
			}
		},
		grid: {
			containLabel: true,
			left: "0",
			top: "5%"
		},
		xAxis: [
			{
				type: 'category',
				data: ['UnLevered', 'Levered', 'Post Tax'],
				axisTick: {
					alignWithLabel: true
				},
				axisLabel: {
					show: true,
					rotate: -30,
					margin: 10
				}
			}
		],
		yAxis: [
			{
				type: 'value',
				axisLine: {
					show: true
				}
			}
		],
		series: multiple_dataseries
		
	};

	if (multiple_option && typeof multiple_option === 'object') {
		multiple_myChart.clear();
		multiple_myChart.setOption(multiple_option);
	}

	//Peak Equity
	var peak_dom = document.getElementById("peak_chart");
	var peak_myChart = echarts.init(peak_dom);
	var peak_app = {};
	var peak_option;
	var peak_dataseries = [];
	peak_dataseries = [
		{
			name: 'Current Peak Equity',
			type: 'bar',
			barWidth: barWidth,
			data: cpeakEquity,
			color: '#2ecc71'
		}
	];
	if (process == 1) {
		peak_dataseries.push({
			name: 'Updated Peak Equity',
			type: 'bar',
			barWidth: barWidth,
			data: peakEquity,
			color: "#d2d2d2"
		});
	};
	peak_option = {
		renderer: 'svg',
		tooltip: {
			trigger: 'axis',
			axisPointer: {
				type: 'shadow'
			}
		},
		grid: {
			containLabel: true,
			left: "0",
			top: "5%"
		},
		xAxis: [
			{
				type: 'category',
				data: ['UnLevered', 'Levered', 'Post Tax'],
				axisTick: {
					alignWithLabel: true
				},
				axisLabel: {
					show: true,
					rotate: -30,
					margin: 10
				}
			}
		],
		yAxis: [
			{
				type: 'value',
				axisLine: {
					show: true
				}
			}
		],
		series: peak_dataseries
		
	};

	if (peak_option && typeof peak_option === 'object') {
		peak_myChart.clear();
		peak_myChart.setOption(peak_option);
	}
};