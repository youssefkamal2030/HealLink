
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class DoctorVerifyCodeMessage extends StatelessWidget {
  const DoctorVerifyCodeMessage({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 49),
        child: Text(
          "we’ve sent an SMS with an activation code to your Email",
          textAlign: TextAlign.center,
          style: AppTextStyles.popins400style16LightDarkGrey,
        ),
      ),
    );
  }
}
