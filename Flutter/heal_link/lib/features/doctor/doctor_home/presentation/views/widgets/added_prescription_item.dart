import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_home/data/models/add_prescription_model.dart';

class AddedPrescriptionItem extends StatelessWidget {
  const AddedPrescriptionItem({
    super.key,
    required this.addPrescriptionModel,
    required this.index,
  });

  final AddPrescriptionModel addPrescriptionModel;
  final int index;

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 332,
      child: Text.rich(
        TextSpan(
          children: [
            TextSpan(
              text: '$index- Medicine Name: ',
              style: AppTextStyles.popins500style12BlackColor,
            ),
            TextSpan(
              text: '${addPrescriptionModel.medicineName}\n',
              style: AppTextStyles.popins500style12BlackColor.copyWith(
                color: AppColors.kPrimaryColor,
              ),
            ),
            TextSpan(
              text: '    Frequency of Use: ',
              style: AppTextStyles.popins500style12BlackColor,
            ),
            TextSpan(
              text: '${addPrescriptionModel.frequencyofUse} daily\n    ',
              style: AppTextStyles.popins500style12BlackColor.copyWith(
                color: AppColors.kPrimaryColor,
              ),
            ),
            TextSpan(
              text: 'Usage Instructions: ',
              style: AppTextStyles.popins500style12BlackColor,
            ),
            TextSpan(
              text: '${addPrescriptionModel.usageInstructions}',
              style: AppTextStyles.popins500style12BlackColor.copyWith(
                color: AppColors.kPrimaryColor,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
