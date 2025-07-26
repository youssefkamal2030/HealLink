
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/features/doctor/doctor_subscription/data/models/subscription_container_model.dart';

class SubscriptionGrowthContainer extends StatelessWidget {
  const SubscriptionGrowthContainer({
    super.key,

    required this.subscriptionContainerModel,
  });
  final SubscriptionContainerModel subscriptionContainerModel;
  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Container(
        padding: EdgeInsets.fromLTRB(20, 20, 20, 15),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(8),
          gradient: LinearGradient(
            begin: Alignment.topRight,
            end: Alignment.center,
            colors: [Color(0xffDBEBFF), Color(0xffF4F6F8)],
          ),
        ),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text(
              subscriptionContainerModel.title,
              textAlign: TextAlign.center,
              style: AppTextStyles.popins500style14BlackColor,
            ),
            SizedBox(height: 8),
            Text(
              subscriptionContainerModel.totalSubscription,
              textAlign: TextAlign.center,
              style: AppTextStyles.popins500style14BlackColor.copyWith(
                fontSize: 18,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
