
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

class DoctorQrSubTitleWidget extends StatelessWidget {
  const DoctorQrSubTitleWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 27.0),
        child: Text(
          "show this code to the patient, to be able to access your profile",
          textAlign: TextAlign.center,
          style: AppTextStyles.popins400style16LightDarkGrey.copyWith(
            color: AppColors.kPrimaryColor,
          ),
        ),
      ),
    );
  }
}
