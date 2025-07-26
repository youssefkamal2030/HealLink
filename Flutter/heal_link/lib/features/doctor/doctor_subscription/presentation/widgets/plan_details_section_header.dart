
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class PlanDetailsSectionHeader extends StatelessWidget {
  const PlanDetailsSectionHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return Text(
      "Plan details",
      style: AppTextStyles.popins500style14BlackColor.copyWith(fontSize: 20),
    );
  }
}
