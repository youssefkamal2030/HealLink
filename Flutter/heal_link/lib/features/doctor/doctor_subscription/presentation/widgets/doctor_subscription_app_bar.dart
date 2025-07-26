
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class DoctorSubscriptionAppBar extends StatelessWidget {
  const DoctorSubscriptionAppBar({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Text(
        "Subscription",
        textAlign: TextAlign.center,
        style: AppTextStyles.popins500style18LightBlackColor.copyWith(
          fontSize: 20,
        ),
      ),
    );
  }
}
