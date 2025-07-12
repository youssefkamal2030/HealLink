
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorVerifyEmailEnterCode extends StatelessWidget {
  const DoctorVerifyEmailEnterCode({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: Text(
        S.of(context).enter_code,
        textAlign: TextAlign.center,
        style: AppTextStyles.roboto500style14BlackColor.copyWith(fontSize: 20),
      ),
    );
  }
}
