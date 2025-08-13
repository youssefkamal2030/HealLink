
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_subscription/data/models/plan_details_model.dart';

class PlanDetailsCustomContainer extends StatelessWidget {
  const PlanDetailsCustomContainer({super.key, required this.planDetailsModel});
 final PlanDetailsModel planDetailsModel ; 
  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: Container(
        padding: EdgeInsets.all(8),
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
          spacing: 16,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            Text(
              planDetailsModel.month,
              style: AppTextStyles.popins500style18LightBlackColor,
            ),
            Text(
              planDetailsModel.description,
              textAlign: TextAlign.center,
              style: AppTextStyles.popins400style12LightBlackColor.copyWith(
                color: AppColors.kDarkGreyColor,
              ),
            ),
            Text(
              planDetailsModel.price,
              textAlign: TextAlign.center,
              style: AppTextStyles.popins500style14BlackColor.copyWith(
                color: AppColors.kPrimaryColor,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
