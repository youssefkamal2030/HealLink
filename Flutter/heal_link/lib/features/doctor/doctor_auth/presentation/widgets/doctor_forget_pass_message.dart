
import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class DoctorForgetPassswordPleaseMessage extends StatelessWidget {
  const DoctorForgetPassswordPleaseMessage({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Text(
        "Please Enter Your Email Address T Receive a verification code",
        textAlign: TextAlign.center,
        style: AppTextStyles.popins500style18LightBlackColor,
      ),
    );
  }
}
