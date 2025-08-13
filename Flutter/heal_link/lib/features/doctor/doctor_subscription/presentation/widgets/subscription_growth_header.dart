
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/subscription_monthly_drop_down.dart';

class SubscribtionGrowthHeader extends StatelessWidget {
  const SubscribtionGrowthHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        Text(
          "Subscribers Growth",
          style: AppTextStyles.popins500style14BlackColor.copyWith(
            fontSize: 20,
          ),
        ),
        Spacer(),
        SubscribtionMonthlyDropdown(),
      ],
    );
  }
}
