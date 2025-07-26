
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/subscribtion_grow_chart.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/subscription_growth_header.dart';

class SubscriptionGrowthSectionOfChart extends StatelessWidget {
  const SubscriptionGrowthSectionOfChart({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Container(
        padding: EdgeInsets.fromLTRB(8, 8, 8, 3),

        decoration: BoxDecoration(
          boxShadow: [
            BoxShadow(
              color: AppColors.kBlackColor.withValues(alpha: 0.1),
              blurRadius: 10,
              offset: Offset(0, 2),
            ),
          ],
          color: AppColors.kWhiteColor,
          borderRadius: BorderRadius.circular(8),
        ),
        child: Column(
          children: [
            SubscribtionGrowthHeader(),
            SizedBox(height: 17),
            SubscribersGrowthChart(),
          ],
        ),
      ),
    );
  }
}
