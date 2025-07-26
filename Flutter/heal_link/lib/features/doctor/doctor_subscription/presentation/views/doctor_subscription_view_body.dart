import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_sliver_height_16.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/doctor_subscription_app_bar.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/plan_details_section.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/subscription_growth_section.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/subscription_growth_section_of_chart.dart';

class DoctorSubscriptionViewBody extends StatelessWidget {
  const DoctorSubscriptionViewBody({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: Padding(
          padding: EdgeInsetsGeometry.symmetric(horizontal: 16),
          child: CustomScrollView(
            slivers: [
              CustomSliverHeight(24),
              DoctorSubscriptionAppBar(),
              CustomSliverHeight(24),
              SubscribtionGrowthSection(),
              CustomSliverHeight(24),
              SubscriptionGrowthSectionOfChart(),
              CustomSliverHeight(24),
              PlanDetailsSection(),
            ],
          ),
        ),
      ),
    );
  }
}
