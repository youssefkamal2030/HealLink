
import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_subscription/data/models/subscription_container_model.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/subscription_custom_container.dart';

class SubscribtionGrowthSection extends StatelessWidget {
  const SubscribtionGrowthSection({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(

   child  : IntrinsicHeight(
      child: Row(
        spacing: 16,
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: List.generate(
          subscriptionContainerModels.length, // number of items
          (index) => SubscriptionGrowthContainer(
            subscriptionContainerModel: subscriptionContainerModels[index],
          ),
        ),
      ),
    ), 

     
    );
  }
}

