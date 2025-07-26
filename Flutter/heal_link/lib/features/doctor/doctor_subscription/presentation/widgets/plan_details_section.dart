
import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/plan_details_section_body.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/plan_details_section_header.dart';

class PlanDetailsSection extends StatelessWidget {
  const PlanDetailsSection({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          PlanDetailsSectionHeader(),
          SizedBox(height: 16),
    
          PlanDetailsSectionBody(),
        ],
      ),
    );
  }
}
