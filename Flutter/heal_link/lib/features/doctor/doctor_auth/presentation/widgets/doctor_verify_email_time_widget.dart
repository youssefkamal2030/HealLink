
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_styles.dart';

class DoctorVerifyEmailTimeWidget extends StatelessWidget {
  const DoctorVerifyEmailTimeWidget({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Text(
        "00:30",
        textAlign: TextAlign.center,
        style: AppTextStyles.popins500style18LightBlackColor.copyWith(
          fontSize: 14,
        ),
      ),
    );
  }
}
