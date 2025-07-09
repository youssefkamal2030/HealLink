
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class DoctorResetPasswordMessage extends StatelessWidget {
  const DoctorResetPasswordMessage({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Text(
        "Your New Password Must Be Differen From previous password  ",
        textAlign: TextAlign.center,
        style: AppTextStyles.popins500style18LightBlackColor,
      ),
    );
  }
}
