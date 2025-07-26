import 'package:fl_chart/fl_chart.dart';
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class SubscribersGrowthChart extends StatelessWidget {
  const SubscribersGrowthChart({super.key});

  @override
  Widget build(BuildContext context) {
    return AspectRatio(
      aspectRatio: 1.8,
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 8.0),
        child: LineChart(
          LineChartData(
            titlesData: FlTitlesData(
              show: true,
              bottomTitles: AxisTitles(
                sideTitles: SideTitles(
                  showTitles: true,
                  reservedSize: 20,
        
                  getTitlesWidget: (value, meta) {
                    const months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'];
                    return Text(
                      months[value.toInt()],
                      style: AppTextStyles.popins600style14PrimaryColor.copyWith(
                        color: AppColors.kDarkGreyColor,
                        fontSize: 12,
                      ),
                    );
                  },
                  interval: 1,
                ),
              ),
              leftTitles: AxisTitles(sideTitles: SideTitles(showTitles: false)),
              topTitles: AxisTitles(sideTitles: SideTitles(showTitles: false)),
              rightTitles: AxisTitles(sideTitles: SideTitles(showTitles: false)),
            ),
            gridData: FlGridData(show: false),
            borderData: FlBorderData(show: false),
            minX: 0,
            maxX: 5,
            minY: 0,
            maxY: 10,
            lineBarsData: [
              LineChartBarData(
                spots: [
                  FlSpot(0, 4),
                  FlSpot(1, 5),
                  FlSpot(2, 3),
                  FlSpot(3, 9),
                  FlSpot(4, 5),
                  FlSpot(5, 6),
                ],
                isCurved: true,
                dotData: FlDotData(
                  show: true,
                  getDotPainter: (spot, percent, barData, index) {
                    if (index == 3) {
                      return FlDotCirclePainter(
                        radius: 5,
                        color: Color(0xff2C7BE5),
                        strokeColor: Colors.white,
                        strokeWidth: 4,
                      );
                    }
                    return FlDotCirclePainter(radius: 0);
                  },
                ),
                belowBarData: BarAreaData(
                  show: true,
                  gradient: LinearGradient(
                    colors: [
                      Color(0xff2C7BE5).withOpacity(0.4),
                      Colors.white.withOpacity(0.0),
                    ],
                    begin: Alignment.topCenter,
                    end: Alignment.bottomCenter,
                  ),
                ),
                gradient: LinearGradient(
                  colors: [Color(0xff2C7BE5), Color(0xff2C7BE5)],
                ),
                isStrokeCapRound: true,
                barWidth: 3,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
