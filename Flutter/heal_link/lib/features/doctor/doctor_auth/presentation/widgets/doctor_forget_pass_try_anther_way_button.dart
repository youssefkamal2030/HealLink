import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorForgetPassTryAntherWayButton extends StatelessWidget {
  const DoctorForgetPassTryAntherWayButton({super.key});

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {},
      child: Container(
        decoration: BoxDecoration(
          border: BoxBorder.fromLTRB(
            bottom: BorderSide(color: AppColors.kPrimaryColor, width: 1),
          ),
        ),
        child: Text(
          S.of(context).try_another_way,

          style: AppTextStyles.popins400style12LightBlackColor.copyWith(
            color: AppColors.kPrimaryColor,
          ),
        ),
      ),
    );
  }
}
