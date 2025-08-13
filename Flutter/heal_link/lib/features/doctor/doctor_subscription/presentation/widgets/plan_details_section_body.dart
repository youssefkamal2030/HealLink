import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_subscription/data/models/plan_details_model.dart';
import 'package:heal_link/features/doctor/doctor_subscription/presentation/widgets/plan_details_custom_container.dart';

class PlanDetailsSectionBody extends StatelessWidget {
  const PlanDetailsSectionBody({super.key});

  @override
  Widget build(BuildContext context) {
    return IntrinsicHeight(
      child: Row(
        spacing: 16,
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: List.generate(
          planDetailsList.length, // number of items
          (index) => PlanDetailsCustomContainer(
            planDetailsModel: planDetailsList[index],
          ),
        ),
      ),
    );
  }
}


/** 
 

 return Row(
      children: [
        PlanDetailsCustomContainer(),
        SizedBox(width: 16),
        PlanDetailsCustomContainer(),
        SizedBox(width: 16),
        PlanDetailsCustomContainer(),
      ],
    );

*/